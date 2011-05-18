function ver([string]$a, [string]$b)
{
	type ("~\My documents\Downloads\"+$a) | .\GCJSolver | Compare-Object (type $b)
}

function gen([string]$a)
{
	type ("~\My documents\Downloads\"+$a) | .\GCJSolver > ($a+".out")
}
