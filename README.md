# dbtail
"Tail" a database table or query like you can tail a file.

## Download exe
You can download the latest built exe from the repo in `dbtail/bin/Relase/dbtail.exe`

## Usage

    Usage: dbtail -h -s <String> -d <String> -c <String> -t <String> -q <String> -f <String> -p <Integer>
    Options:
     -s --server <String>           : Database Server [localhost]
     -d --database <String>         : Database Name
     -c --connectionstring <String> : Connection String (Overrides --server and --database)
     -t --table <String>            : Table Name to Query
     -q --query <String>            : Custom SQL Query (Overrides --table)
     -f --format <String>           : Custom String.Format output
     -p --pollinterval <Integer>    : Poll Interval in milliseconds [200]
     -h --help                      : This help
