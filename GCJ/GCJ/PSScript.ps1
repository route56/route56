function ver([string]$a, [string]$b)
{
	type ("~\My documents\Downloads\"+$a) | .\GCJSolver | Compare-Object (type $b)
}

function gen([string]$a)
{
	type ("~\My documents\Downloads\"+$a) | .\GCJSolver > ($a+".out")
}


function hm()
{
	pushd D:\Route56\GCJ\GCJ\GCJSolver\bin\Debug
}

function dw()
{
	pushd ("~\My documents\Downloads\")
}


