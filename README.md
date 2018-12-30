# dxfTool
Simple tool to create dxf file with circles inside that locations are defined in separate file. All circles has the same radius.  This project uses netDxf library

usage:  
dxfTool.exe path-to-source-fie radius [path-to-output-dxf-fie]
  
1. each line in source file contains coordinates of cirle centre with comma as separator. Following three lines for three circles examplexample:  

~~~~
42.02500,24.57500,0.00000  
9.27000,16.41000,-3.95500  
55.69500,20.11500,0.38000   
~~~~

2. radius with double precision 

3. third parameter is optional name and path to output file. Name of source file (argument nr 1) with extension dxf will be used if this parameter is not specified. 
