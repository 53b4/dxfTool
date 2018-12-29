# dxfTool
Simple tool to create dxf file with circles inside that locations are defined in separate file. All circles has the same radius.  This project uses on netDxf library

usage: 
dxfTool.exe path-to-source-fie radius [path-to-output-dxf-fie]

- each line in source file contains coordinates of cirle centre with following format. Comma is used as separator. Following three lines for three circles examplexample 

42,025 24,575 0
9,27 16,41 -3,955
55,695 20,115 0,38

- radius with double precision 

- third parameter is optional name and path of output file. Input name with extension dxf will be used if this parameter is not specified. 