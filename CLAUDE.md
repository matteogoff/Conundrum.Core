# CLAUDE.md — Conundrum.Core

Guidance for working in the `Conundrum.Core` solution. See [README.md](README.md)
for the architectural overview; this file focuses on conventions and how to add
code without breaking the layering.

## What this is

`Conundrum.Core` is the dependency-free domain library for the Conundrum cipher
simulator: classic cipher machines (Caesar, Enigma) behind a shared
`ICipherMachine` contract, plus serializable `Conundrum.Model` settings used for
persistence (MongoDB) and API transfer. It is consumed by
`conundrum-ui-angular.Server`, but must not depend on the web layer.

## Project map

- **`Conundrum.Model/`** (`Conundrum.Model`) — settings/DTOs. Base layer; no
  internal dependencies. References `MongoDB.Bson`, `Newtonsoft.Json`.
- **`Conundrum.Crypto/`** (`Conundrum.Crypto`) — `ICipherMachine`, `CipherBase`.
  References `Model`.
- **`Conundrum.Ceasar/`** (`Conundrum.Ceasar`) — `CeasarMachine`. References
  `Crypto`, `Model`.
- **`Conundrum.Enyigma/`** (`Conundrum.Enigma`) — `EnigmaMachine`, `Rotor`,
  `Reflector`, `PlugBoard`. References `Crypto`, `Model`.
- **`Tests/`** — xUnit projects, one per cipher.

> The Enigma **folder** is `Conundrum.Enyigma` (typo), but the **project,
> assembly, and namespace** are `Conundrum.Enigma`. Match the folder only in
> paths; use `Conundrum.Enigma` in code and `using` directives.

## Layering rules (do not violate)

```
Model  ◄──  Crypto  ◄──  Ceasar
                    ◄──  Enigma
```

- Dependencies point inward toward `Model`. `Model` references nothing internal.
- Cipher projects (`Ceasar`, `Enigma`) must **not** reference each other — only
  `Crypto` and `Model`.
- Keep the web/server concerns out of this library entirely.

## Build & test

From the `Conundrum.Core` directory:

```bash
dotnet build Conundrum.Core.sln
dotnet test  Conundrum.Core.sln
```

All projects target **.NET 8** with `<ImplicitUsings>enable</ImplicitUsings>`
and `<Nullable>enable</Nullable>`. Don't add `using` lines that implicit usings
already cover.

## Conventions

- **XML doc comments** on public types and members — this codebase documents
  heavily; match it.
- **`Debug.WriteLine`** for trace logging inside encode/rotate paths (see
  `Rotor`, `EnigmaMachine`). No logging framework is used here.
- **`[Serializable]`** on every `Conundrum.Model` type.
- Models that persist to MongoDB implement `IContainerCollection` and expose a
  `CollectionName` (e.g. `"Ciphers"`, `"Users"`, `"Rotors"`).
- Cipher machines are stateful: `Encode` mutates rotor position; always provide a
  working `Reset()`.
- `internal` members exposed to tests go through
  `[assembly: InternalsVisibleTo(...)]` (see `Conundrum.Enyigma/AssemblyInfo.cs`).

## How to add code

### A new cipher machine

1. Create a class library under `Conundrum.Core/` (e.g. `Conundrum.Vigenere/`),
   .NET 8, implicit usings + nullable enabled. Add ProjectReferences to
   `Conundrum.Crypto` and `Conundrum.Model` only.
2. Make the machine class derive from `CipherBase` and implement
   `ICipherMachine`: `Encode`, `Reset`, `GetSettings`, `GetPositions`. Honor
   `ByPassCharacters` in `Encode` (return the input unchanged when it's a bypass
   character).
3. Add a matching `*MachineSetting : CipherSetting` in `Conundrum.Model`, set its
   `Type` string, and add a constructor on the machine that rebuilds it from that
   setting — preserve the build → `GetSettings()` → persist → reload round trip.
4. Add the project to `Conundrum.Core.sln`
   (`dotnet sln Conundrum.Core.sln add <path>`).
5. Add a `Tests/<Name>.Test.Unit/` xUnit project (mirror an existing test
   `.csproj`) and register it in the solution.
6. If the server should expose it, add the ProjectReference in
   `conundrum-ui-angular.Server/conundrum-ui-angular.Server.csproj` and wire it
   into the relevant factory there.

### A new Enigma component or setting

- Component (rotor/reflector/plugboard variant) → `Conundrum.Enyigma/`, with a
  `GetSettings()` returning a `Conundrum.Model` setting and a constructor that
  accepts that setting.
- New persisted/transferred shape → add to `Conundrum.Model` as a
  `[Serializable]` class; if it maps to a collection, implement
  `IContainerCollection`.

### Tests

- xUnit, `[Fact]`/`[Theory]`, Arrange/Act/Assert with comment markers (match
  existing tests). Reference the production project(s) under test.
- Encode/decode symmetry is the key invariant: a value encoded by a machine is
  recovered by an identically configured, reset machine. Cover rotor turnover at
  notches for Enigma.
