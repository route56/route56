#xsd.exe .\TestFile.xml
#if($?)
#{
#	"Done xsd"
#	.\XmlGen.exe .\TestFile.xsd
#	"Done xmlgen"
#}

#.\XmlGen.exe .\Final.xsd
#copy .\Sample.xml .\Final.xml
