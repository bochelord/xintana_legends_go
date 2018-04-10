﻿using UnityEngine;

namespace I2.Loc
{
	public static class ScriptLocalization
	{
		public static string Get(string Term, bool FixForRTL=true, int maxLineLengthForRTL=0, bool ignoreRTLnumbers=true, bool applyParameters=false, GameObject localParametersRoot=null, string overrideLanguage=null) { return LocalizationManager.GetTranslation(Term, FixForRTL, maxLineLengthForRTL, ignoreRTLnumbers, applyParameters, localParametersRoot, overrideLanguage); }


		public static string si_yellowsword 		{ get{ return Get ("si_yellowsword"); } }
		public static string si_x2 		{ get{ return Get ("si_x2"); } }
		public static string si_supportdevs 		{ get{ return Get ("si_supportdevs"); } }
		public static string si_shell04 		{ get{ return Get ("si_shell04"); } }
		public static string si_shell03 		{ get{ return Get ("si_shell03"); } }
		public static string si_shell02 		{ get{ return Get ("si_shell02"); } }
		public static string si_shell01 		{ get{ return Get ("si_shell01"); } }
		public static string si_greensword 		{ get{ return Get ("si_greensword"); } }
		public static string si_goldchest 		{ get{ return Get ("si_goldchest"); } }
		public static string si_goldcave 		{ get{ return Get ("si_goldcave"); } }
		public static string si_goldbig 		{ get{ return Get ("si_goldbig"); } }
		public static string si_bluesword 		{ get{ return Get ("si_bluesword"); } }
		public static string si_blacksword 		{ get{ return Get ("si_blacksword"); } }
		public static string si_5gems 		{ get{ return Get ("si_5gems"); } }
		public static string si_20gems 		{ get{ return Get ("si_20gems"); } }
		public static string si_1up 		{ get{ return Get ("si_1up"); } }
		public static string si_1gem 		{ get{ return Get ("si_1gem"); } }
		public static string si_10gems 		{ get{ return Get ("si_10gems"); } }

	}
}