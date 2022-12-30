module App

open System
open System.IO
open System.Text
open System.Xml
open System.Xml.XPath
open System.Xml.Xsl

open CommonTools
open ColorPrint

type private Xslt1Options = {
  XslFile: string
  XmlFile: string
  OutFile: string
  OutExt: string
}

let private parseArgs args =
  let o = {
    XslFile = null
    XmlFile = null
    OutFile = null
    OutExt = null
  }
  let rec parseMore o args =
    match args with
    | "-v" :: rest ->
      rest |> parseMore o
    | "-d" :: xmlFile :: rest ->
      rest |> parseMore {o with XmlFile=xmlFile}
    | "-s" :: xslFile :: rest ->
      rest |> parseMore {o with XslFile=xslFile}
    | "-o" :: outFile :: rest ->
      rest |> parseMore {o with OutFile=outFile}
    | "-O" :: outExt :: rest ->
      if outExt.StartsWith('.') |> not then
        failwith "The argument to -O must start with a '.'"
      rest |> parseMore {o with OutExt = outExt}
    | [] ->
      if o.XmlFile |> String.IsNullOrEmpty then
        failwith "No source document specified"
      let o =
        if o.OutFile |> String.IsNullOrEmpty then
          let outExt =
            if o.OutExt |> String.IsNullOrEmpty then
              ".out.xml"
            else
              o.OutExt
          {o with OutFile = Path.ChangeExtension(o.XmlFile, outExt)}
        else
          o
      // leave handling of a missing stylesheet until later
      o
    | x :: _ ->
      failwithf "Unrecognized option: '%s'" x
  args |> parseMore o

let runApp args =
  // Bring support for code pages back to .net framework level (pre .net core)
  Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)
  let o = args |> parseArgs
  let doc = new XPathDocument(o.XmlFile)
  let xslFile =
    if o.XslFile |> String.IsNullOrEmpty then
      cp "\frNot Yet Implemented\fo "
      failwith "NYI - extracting style sheet name from document"
    else
      o.XslFile
  let transform =
    let trx = new XslCompiledTransform()
    trx.Load(xslFile)
    trx
  do
    use tw = o.OutFile |> startFile
    let arglist = new XsltArgumentList()
    transform.Transform(doc, arglist, tw)
  o.OutFile |> finishFile
  0



