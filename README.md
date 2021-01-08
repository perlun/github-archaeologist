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

## Further reading

- [Understanding how GitHub uses and protects your data](https://docs.github.com/en/free-pro-team@latest/github/understanding-how-github-uses-and-protects-your-data)
