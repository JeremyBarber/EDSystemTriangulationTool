# Contributing

Please feel to raise MRs to fix or add things to this little app.

## Constructing a Pull Request

When constructing a pull request, please follow the instructions below:

### Branch

- Raise MRs against the `develop` branch. This will ensure that your changes are packaged correctly for the next release

### Changelog

- Add a description of your changes to the `Unreleased` section of the `CHANGELOG`.

- The formatting of this section should follow the formatting of [Keep a Changelog](https://keepachangelog.com/en/1.0.0/), although you can look at previous entries to get an idea.

- Lines added to the `CHANGELOG` should start with `(Major)`, `(Minor)` or, `(Patch)` to mark their severity according to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

- The lines added to `CHANGELOG` should be copied into the pull request description

## Version numbers

Version numbers are entirely automated through GitHub Actions. Please do not attempt to override or manually change these.

Instead, simply follow the rules above and everything will be taken care of. In the event of a disaster requiring manual intervention, please contact the repo administrator.