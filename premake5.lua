newoption {
	trigger = "deps",
	description = "Relative location to dependencies.",
	value = "DIRECTORY",
	default = "Dependencies/",
}

function local_links(l)
	local dep_dir = _OPTIONS["deps"]

	for i = 1, #l do
		links { path.join(dep_dir, l[i]) }
	end
end

solution "ItsyRealm"
	configurations { "Debug", "Release" }
	platforms { "x86", "x64" }

	project "ItsyRealm.Launcher"
		kind "WindowedApp"
		language "C#"
		location "Launcher"
		icon "ItsyRealm.ico"
		files { "Launcher/**.cs" }

		links { "System" }
		links { "System.Windows.Forms" }
		local_links { "INIFileParser" }
		local_links { "ICSharpCode.SharpZipLib" }
		local_links { "Newtonsoft.Json" }

		configuration "Debug"
			targetdir "Build/Binaries/Debug"
			objdir "Build/Objects/Launcher/Debug"
			defines { "ITSYREALM_BUILD_DEBUG" }
		configuration "Release"
			targetdir "Build/Binaries/Release"
			objdir "Build/Objects/Launcher/Release"
			defines { "ITSYREALM_BUILD_RELEASE" }

		configuration {}
		postbuildcommands {
			"{RMDIR} \"%{!cfg.targetdir}/Fused\"",
			"{MKDIR} \"%{!cfg.targetdir}/Fused\"",
			"ILMerge.exe " ..
				"\"/out:%{!cfg.targetdir}/Fused/ItsyRealm.Launcher.exe\" "..
				"\"%{!cfg.targetdir}/ItsyRealm.Launcher.exe\" " ..
				"\"%{!cfg.targetdir}/*.dll\" " ..
				"/target:winexe " ..
				"/targetplatform:\"v4,C:\\Program Files (x86)\\Reference Assemblies\\Microsoft\\Framework\\.NETFramework\\v4.5\" " ..
				"/wildcards"
		}
