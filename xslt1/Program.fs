// (c) 2022  ttelcl / ttelcl

open System
open System.Text

open CommonTools
open ExceptionTool
open Usage

let rec run arglist =
  // For subcommand based apps, split based on subcommand here
  match arglist with
  | "-v" :: rest ->
    verbose <- true
    rest |> run
  | "--help" :: _
  | "-h" :: _
  | [] ->
    usage verbose
    0  // program return status code to the operating system; 0 == "OK"
  //  *EXAMPLE*:
  //| "foo" :: rest ->
  //  rest |> AppFoo.runFoo
  | _ :: _ ->
    arglist |> App.runApp

[<EntryPoint>]
let main args =
  try
    args |> Array.toList |> run
  with
  | ex ->
    ex |> fancyExceptionPrint verbose
    resetColor ()
    1



