# aanki-workflows

Reusable GitHub Actions workflows every other repo calls. Its spec is [docs/spec/repos/aanki-workflows.md](../docs/spec/repos/aanki-workflows.md) in the aanki-harness workspace; start sessions at the workspace root so the harness's shared rules load too.

## Commands

Run from this folder. Workflows cannot run locally; [ci.yml](.github/workflows/ci.yml) runs all three on every push and pull request. There is no deploy and no smoke check, since nothing here deploys.

| Command | Does |
|---|---|
| `actionlint` | Lints every workflow ([install](https://github.com/rhysd/actionlint)) |
| `gitleaks git --redact` | Scans history; the pre-commit hook scans each commit |
| `dotnet build; dotnet test` in `fixtures/dotnet` | The fixture the .NET workflow runs on |

## Layout

- `.github/workflows/`: `ci.yml` (this repo's own checks) and one reusable workflow per row of the spec's table.
- `fixtures/dotnet/`: a one-test solution; `ci.yml` calls `dotnet-build-test.yml` on it.

## Quirks

- A caller's job must grant what the workflow it calls asks for: `id-token: write` for the AWS ones, `pull-requests: write` for `terraform-plan.yml`.
- The role ARN arrives as the secret `AWS_ROLE_ARN`, so the account ID stays out of public logs.
- Third-party actions are pinned to a commit SHA with the version in a comment; tools downloaded in a job are pinned by version and SHA-256.
- `deploy-lambda.yml` builds on an arm64 runner; the Lambda's assembly name is `bootstrap`.
- Callers pin `@v1`, a tag moved to the latest compatible commit; a breaking change gets `v2`.
