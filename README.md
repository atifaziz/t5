# T5

[![Build Status][win-build-badge]][win-builds]
[![Build Status][nix-build-badge]][nix-builds]  
[![NuGet][tt-nuget-badge]][tt-nuget-pkg]
[![MyGet][tt-myget-badge]][tt-edge-pkgs]  
[![NuGet][nuget-badge]][nuget-pkg]
[![MyGet][myget-badge]][edge-pkgs]

T5 is an open-source implementation of the T4 text templating engine
for .NET Core based on and derived from [Mono.TextTemplating][mono-tt].


## Usage

The TextTransform tool can be installed into a .NET Core 2.0 project by adding
a `<DotNetCliToolReference>` node to the project file, as shown below:

```xml
  <ItemGroup>
    <DotNetCliToolReference Include="T5.TextTransform.Tool"
                            Version="1.1.0-*" />
  </ItemGroup>
```

Set the `Version` attribute value to the desired version of the tool.

The reference will cause the tool to be installed whenever `dotnet restore`
is run next. It can then be used as follows:

    dotnet tt TEMPLATE

Replace `TEMPLATE` with the path to your T4 template.

For full help on usage, run:

    dotnet tt --help

When you run `dotnet tt`, make sure that you do so from the project's
directory set as your shell's current directory otherwise `dotnet` will
complain with an error message along the lines of:

    No executable found matching command "dotnet-tt"


## Building

Make sure that the .NET SDK Core 2.0 is installed.

To build the project, run `build.cmd` on Windows or `build.sh` on macOS or a
supported Linux distribution.

To run the unit tests, run `test.cmd` on Windows or `test.sh` on macOS or a
supported Linux distribution. The test script builds the project before
running the unit tests.

To build NuGet packages for distribution, run `pack.cmd` on Windows or
`pack.sh` on macOS or a supported Linux distribution. The packaging script
builds the project but does not run unit tests. The script accepts a single
optional argument that is used as the version suffix of the packages, e.g.
`beta1`.


[win-build-badge]: https://img.shields.io/appveyor/ci/raboof/t5/master.svg?label=windows
[win-builds]: https://ci.appveyor.com/project/raboof/t5
[nix-build-badge]: https://img.shields.io/travis/atifaziz/t5/master.svg?label=linux
[nix-builds]: https://travis-ci.org/atifaziz/t5
[myget-badge]: https://img.shields.io/myget/raboof/vpre/T5.TextTemplating.svg?label=myget+|+lib
[edge-pkgs]: https://www.myget.org/feed/raboof/package/nuget/T5.TextTemplating
[nuget-badge]: https://img.shields.io/nuget/v/T5.TextTemplating.svg?label=nuget+|+lib
[nuget-pkg]: https://www.nuget.org/packages/T5.TextTemplating
[tt-myget-badge]: https://img.shields.io/myget/raboof/vpre/T5.TextTransform.Tool.svg?label=myget+|+tt
[tt-edge-pkgs]: https://www.myget.org/feed/raboof/package/nuget/T5.TextTransform.Tool
[tt-nuget-badge]: https://img.shields.io/nuget/v/T5.TextTransform.Tool.svg?label=nuget+|+tt
[tt-nuget-pkg]: https://www.nuget.org/packages/T5.TextTransform.Tool

[mono-tt]: https://www.nuget.org/packages/Mono.TextTemplating
