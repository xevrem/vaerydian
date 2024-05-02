{
  description = "load c# dev env for godot4-mono";

  inputs = {
    nixpkgs.url = "github:nixos/nixpkgs?ref=nixos-unstable";
    utils.url = "github:numtide/flake-utils";
  };

  outputs = { self, nixpkgs, utils }:
    utils.lib.eachDefaultSystem (system:
      let
        pkgs = import nixpkgs { inherit system; };
        dotnetPkgs = (with pkgs.dotnetCorePackages; combinePackages [
          runtime_6_0
          runtime_7_0
          runtime_8_0
          sdk_6_0
          sdk_7_0
          sdk_8_0
        ]);
        nativeDeps = with pkgs; [
          dotnetPkgs
        ];
        xDeps = with pkgs.xorg; [
          libX11
          libXcursor
          libXinerama
          libXext
          libXrandr
          libXrender
          libXi
          libXfixes
        ];
        runtimePkgs = with pkgs; [
          libpulseaudio
          dbus
          dbus.lib
          fontconfig
          fontconfig.lib
          udev
        ];
        in
        {
          devShell = with pkgs; mkShell rec {
            nativeBuildInputs = [
              pkg-config
            ] ++ nativeDeps;
            buildInputs = [
              vulkan-loader
              libGL
              libxkbcommon
              alsa-lib
            ] ++ runtimePkgs ++ xDeps;
            packages = [
              csharp-ls
              csharpier
            ];
            LD_LIBRARY_PATH = lib.makeLibraryPath buildInputs;
            DOTNET_ROOT = "${dotnetPkgs}";
          };
        }
    );
}
