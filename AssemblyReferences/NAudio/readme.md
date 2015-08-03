# Custom NAudio-r559-v1.7.1.17

We simply signed the assembly.

Using a custom build of NAudio is not necessary with minor exception to that you need to sign the assembly in order for it to compile with this project 'out of the box'.  Any version from r467 forward should be fine if you want to download and compile NAudio yourself.

Implementing `InternalsVisibleToAttribute` can be a pain.  I'd posted a [GIST][gist] to help with the concept.

# NAudio-v1.7.1 NuGet package

Optionally, you may use the NuGet package if you disable signing in all related CSProj files (StrongName).

# Custom NAudio-r467

We use a custom build of NAudio for the following reasons:

- [NAudio does not provide a signed version in its NuGet package][forum]. Included in this readme below is the note from [wolf5370] posted `Aug 21, 2012 at 7:29 AM`.
- Two source-archives are available [here][package_loc].
    - NAudio-r467-mod+MEF-9-mod-source.zip — all sources required for building gen.snd libs and apps.
    - NAudio-r467-changed-files.zip — just the files changed in the particular NAudio revision.

## Note from wolf5370

The DLL can be manually signed without rebuilding (necessary with 1.5 as source is missing the Midi library so won't compile currently) - direct from DLL binary...

- Delete reference in project to NAudio.dll
- Start VS2010 Command Prompt
- CD to NAudio.DLL directory
- Create a random SNK keypair file using `SN.EXE -k NAudio.snk`
- Disasemble NAudio.dll to IL using `ildasm /all /out=NAudio.il NAudio.dll`
- Rebuild IL with strong key pair using ILASM `ilasm /dll /key=NAudio.snk NAudio.il`
- Re-add recference to NAudio.dll in my project
- F6 rebuild in VS2010 - all done

Note: You may want to back up NAudio.dll (as an un-signed version first).

[package_loc]: https://drive.google.com/folderview?id=0B3zU6tYfj4ZHbUg0Y0ZOaV9xWm8&usp=sharing
[forum]: http://naudio.codeplex.com/discussions/287348
[wolf5370]: http://www.codeplex.com/site/users/view/wolf5370
[gist]: https://gist.github.com/tfwio/b357a0b3a75e59d43aed
