using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegexProblems.RegexProblems
{
	class UrlMatcher
	{
		// ((http://)|(www\.)|(http://www\.))([^/]*\.)?regexpal\.com.*
//matched
//http://regexpal.com/some/more/text
//http://regexpal.com/
//http://www.regexpal.com/
//www.regexpal.com/
//http://some.regexpal.com/
//http://foo.bar.regexpal.com/

//unmatched
//http://someregexpal.com/
//http://regexpalbar.com/
//http://some.com/regexpal
//www2.regexpal.com/

	}
}
