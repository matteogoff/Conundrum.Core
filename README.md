# Conundrum.Core

The core domain library for **Conundrum**, a cipher-machine simulator. It provides
.NET implementations of classic encryption devices — a Caesar cipher and a full
Enigma machine — behind a common `ICipherMachine` abstraction, along with the
serializable data models used to persist and transfer cipher configurations.

This solution is consumed by the ASP.NET Core back end
(`conundrum-ui-angular.Server`) that powers the Angular front end, but it has no
dependency on the web layer and can be referenced by any .NET 8 host.

## Solution layout

`Conundrum.Core.sln` contains four class libraries and two test projects, all
targeting **.NET 8** with `ImplicitUsings` and `Nullable` enabled.

| Project | Folder | Purpose |
| --- | --- | --- |
| `Conundrum.Model` | `Conundrum.Model/` | Serializable settings/DTOs and persistence interfaces. The base layer — depends on nothing internal. |
| `Conundrum.Crypto` | `Conundrum.Crypto/` | The `ICipherMachine` contract and `CipherBase` shared base class. |
| `Conundrum.Ceasar` | `Conundrum.Ceasar/` | `CeasarMachine` — a rotating Caesar-shift cipher. |
| `Conundrum.Enigma` | `Conundrum.Enyigma/` | `EnigmaMachine` and its components (`Rotor`, `Reflector`, `PlugBoard`). |
| `Conundrum.Ceasar.Test.Unit` | `Tests/Conundrum.Ceasar.Test.Unit/` | xUnit tests for the Caesar machine. |
| `Conundrum.Enigma.Test.Unit` | `Tests/Conundrum.Enyigma.Tests.Unit/` | xUnit tests for the Enigma machine and its parts. |

> **Note:** the Enigma project lives in a folder spelled `Conundrum.Enyigma`, but
> the project, assembly, and namespace are all `Conundrum.Enigma`. Use the
> `Conundrum.Enigma` name everywhere except file paths.

## Architecture

### Dependency layering

```
Conundrum.Model   (no internal dependencies)
      ▲
Conundrum.Crypto  (-> Model)
      ▲
      ├── Conundrum.Ceasar  (-> Crypto, Model)
      └── Conundrum.Enigma  (-> Crypto, Model)
```

Dependencies point inward toward `Model`. Cipher implementations never reference
each other; they only share the `Crypto` abstraction.

### The cipher contract

Every cipher machine implements `ICipherMachine` (in `Conundrum.Crypto`):

- `char Encode(char input)` — encodes a single character. Characters in
  `ByPassCharacters` are returned unchanged (used for spaces, punctuation, etc.).
  Encoding advances the machine's internal state (rotation) as a side effect.
- `void Reset()` — restores the machine to its starting position.
- `ICipherSetting GetSettings()` — captures the current configuration as a
  serializable model.
- `Dictionary<string, char> GetPositions()` — reports each rotor's current
  position by name.
- `List<char> ByPassCharacters { get; set; }` — characters to pass through untouched.

`CipherBase` is the shared base class; it initializes `ByPassCharacters`. Both
`CeasarMachine` and `EnigmaMachine` derive from `CipherBase` and implement
`ICipherMachine`.

Because the Enigma and Caesar rotor mechanics are symmetric, **encoding and
decoding are the same operation** — feed ciphertext back through an identically
configured machine to recover the plaintext.

### The Enigma pipeline

`EnigmaMachine.Encode` runs each character through:

1. **Rotate** the rotor stack (with notch-driven carry to the next rotor).
2. **Plugboard** swap (`PlugBoard.Map`).
3. **Forward** pass through each `Rotor`.
4. **Reflector** (`Reflector.Reflect`).
5. **Backward** pass through the rotors in reverse order.
6. **Plugboard** swap again.

### The Model layer

`Conundrum.Model` holds the serializable representations used for persistence and
API transfer:

- `ICipherSetting` / `CipherSetting` — base settings (`Name`, `Type`, `Date`,
  `Summary`, Mongo `Id`).
- `CeasarMachineSetting`, `EnigmaMachineSetting` — per-cipher settings.
- `RotorSetting`, `ReflectorSetting`, `PlugBoardSetting` — Enigma component
  settings.
- `Rotor`, `User` — persisted documents.
- `IContainerCollection` — exposes `CollectionName` so models map to MongoDB
  collections.

Models are `[Serializable]` and reference `MongoDB.Bson` (for `ObjectId`) and
`Newtonsoft.Json`. Cipher machines can be reconstructed directly from a settings
object (e.g. `new EnigmaMachine(EnigmaMachineSetting settings)`), enabling a
round trip: build → `GetSettings()` → persist → reload → reconstruct.

## Building and testing

From the `Conundrum.Core` directory:

```bash
dotnet build Conundrum.Core.sln
dotnet test  Conundrum.Core.sln
```

Tests use **xUnit**. The Enigma test project relies on
`[assembly: InternalsVisibleTo("Conundrum.Enigma.Test.Unit")]` to reach
`internal` members of `Rotor`.

## Usage example

```csharp
using Conundrum.Enigma;

var rotors = new List<Rotor>
{
    new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ", "Q", startingPosition: 'A', name: "I"),
};
var reflector = new Reflector("YRUHQSLDPXNGOKMIEBFZCWVJAT");
var plugboard = new PlugBoard(new Dictionary<char, char> { { 'A', 'B' } });

var machine = new EnigmaMachine(rotors, reflector, plugboard);

char cipher = machine.Encode('A');   // advances rotors, returns ciphertext
machine.Reset();                     // back to starting positions
```
