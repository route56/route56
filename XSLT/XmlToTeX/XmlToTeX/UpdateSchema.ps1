#xsd.exe .\TestFile.xml
#if($?)
#{
#	.\XmlGen.exe .\TestFile.xsd
#	copy .\Sample.xml .\TestFile.xml
#}

.\XmlGen.exe .\Final.xsd
copy .\Sample.xml .\Final.xml
