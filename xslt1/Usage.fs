// (c) 2022  ttelcl / ttelcl
module Usage

open CommonTools
open ColorPrint

let usage detailed =
  cp "\foxslt1 [\fg-s \fc<stylesheet.xslt>\f0] \fg-d \fc<data.xml>\f0 [\fg-o \fc<outputfile>\f0|\fg-O \fc.<outputextension>\f0]"
  cp "\fwApply an XSLT 1.0 stylesheet to the source document\f0"
  cp "\fg-s \fc<stylesheet.xslt>\f0    The stylesheet to execute"
  cp "    \fyCurrently this is a required argument\f0 Extracting it from a PI in data is not implemented."
  cp "\fg-d \fc<data.xml>\f0           The data to apply the stylesheet to."
  cp "\fg-o \fc<outputfile>\f0         The output file name."
  cp "\fg-O \fc.<outputextension>\f0   Output file extension (including '.')."
  cp "The output name is determined as follows:"
  cp "- If '\fg-o\f0' is given: its argument"
  cp "- Otherwise, if '\fg-O\f0' is given: the data file with the extension changed"
  cp "- Otherwise: act as if '\fg-O \fc.out.xml\f0' was given"
  
  


