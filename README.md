# github-archaeologist

A simple CLI tool to extract some potentially interesting data from a GitHub export.

## Download and unpack your data

1. Clone this GitHub repo.

2. Follow [this guide in the GitHub docs](https://docs.github.com/en/free-pro-team@latest/github/understanding-how-github-uses-and-protects-your-data/requesting-an-archive-of-your-personal-accounts-data#downloading-an-archive-of-your-personal-accounts-data) to get an export of your GitHub data. Note that it might take some time to generate the export and it can become quite large; in my case, about 6 GiB. Download this file to your computer.

2. `cd data` and extract the data from your export.

   ```shell
   $ tar xvzf ~/Downloads/b0f42ffc-659c-4e68-8873-49b4d656a928.tar.gz --exclude repositories
   ```

3. You should now be able to run the `github-archeologist` tool.

## Running the tool

```shell
$ make run
```

This will give you an output roughly like this:

```
dotnet run --project src/github-archaeologist/github-archaeologist.csproj data 2020
2020-01:
    14, 15, 21, 25: perlun/halleluja.nu
2020-02:
    19, 20: perlun/dotfiles
    3, 11: perlun/halleluja.nu
    24: perlun/perlun.eu.org
    28: perlun/billigastematkassen

[...etc, with data included for all months of the year where there is activity]
```

## Limitations

**Note**: I realized this halfway through implementing this. The data exported using the "download your personal data" is really _only_ your personal data. In other words, it only includes events related to repositories under your https://github.com/username account. It does **not** include (which was my original perception) activity in projects under other users/organizations.

## Supported types of data

- Commit comments
- Issue comments
- Issue events

## Further reading

- [Understanding how GitHub uses and protects your data](https://docs.github.com/en/free-pro-team@latest/github/understanding-how-github-uses-and-protects-your-data)
