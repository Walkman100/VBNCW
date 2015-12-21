# Mono Tests
Test files used to check if you have Mono and Mono-VB installed correctly.

Follow [mono-project.com/docs/getting-started/install/linux](http://www.mono-project.com/docs/getting-started/install/linux/) to install `mono`, and see [mono-project.com/docs/getting-started/mono-basics](http://www.mono-project.com/docs/getting-started/mono-basics/) for where these files come from.

Once you have cloned this repo, you can `cd` into this folder and run `./run-tests.sh` to run all the tests - the script will warn you and stop if there are errors. Note that the second last test requires opening a provided URL in a browser, and the last test doesn't exit when you close it's window, you'll have to use <kbd>Ctrl</kbd> + <kbd>C</kbd>.
