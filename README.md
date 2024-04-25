# WhileTrueProduction

# Directories Explanation
```
|-->Assets: Folder kerja utama
|  |-->Animations: Tempat menyimpan .anim
|  |-->Fonts: Tempat menyimpan variasi huruf
|  |-->Images: Folder Gambar
|     |-->Bgs: Background
|     |-->Characters: Karakter
|     |-->Environments: Benda - benda sekitar
|  |-->Scenes: Folder scene unity
|     |-->Menu: Scene untuk menu dan setting
|     |-->Platformer: Scene level platformer
|     |-->Td: Scene level tower defense
|     |-->Test: Scene coba-coba
|  |-->Scripts: Folder script c#
|     |-->Enemies: Script untuk pergerakan musuh
|     |-->Environments: Script untuk benda - benda
|     |-->Npcs: Script untuk karakter non playable
|     |-->Player: Script untuk karakter playable
|  |-->Sounds: Folder audio
|     |-->Bgms: Suara background
|     |-->Sfxs: Suara ceting2
|-->Packages
|-->ProjectSettings

```

# Sematic Commit Messages

## Commit Type
- `feat`: (new feature for the user, not a new feature for build script)
- `fix`: (bug fix for the user, not a fix to a build script)
- `docs`: (changes to the documentation)
- `style`: (formatting, missing semi colons, etc; no production code change)
- `refactor`: (refactoring production code, eg. renaming a variable)
- `test`: (adding missing tests, refactoring tests; no production code change)
- `chore`: (updating grunt tasks etc; no production code change)

## Example

```
feat: animate player idling
^--^  ^------------^
|     |
|     +-> Summary in present tense.
|
+-------> Type: chore, docs, feat, fix, refactor, style, or test.
```