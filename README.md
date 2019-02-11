# ItsyRealm Launcher
This is the source code for the ItsyRealm launcher for Windows.

# Dependencies
ItsyRealm launcher requires .NET frame 4.5 and the following dependencies:

* [SharpZipLib](https://github.com/icsharpcode/SharpZipLib)
* [Json.NET](https://www.newtonsoft.com/json)
* [ini-parser](https://github.com/rickyah/ini-parser)
* [Markdown](https://github.com/hey-red/Markdown)

Place the DLLs in `Dependencies` or pass the directory they are located to via
`--deps=<path>`.

ILMerge must be in your path to build a single executable.

# Building
Run `premake5` with your build system of choice. See above for how to specify
dependencies.

# License & Copyright

Copyright (c) Aaron Bolyard

This project is licensed under the MPL. View LICENSE in the root directory or
visit http://mozilla.org/MPL/2.0/ for the terms.