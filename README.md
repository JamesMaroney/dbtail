# dbtail
"Tail" a database table or query like you can tail a file.

## Download exe
You can download the latest built exe from the repo in `dbtail/bin/Relase/dbtail.exe`

## Usage

    Usage: dbtail -frh -S <String> -d <String> -c <String> -t <String> -q <String> -F <String> -s <Integer>
        Options:
        -S --server <String>           : Database Server [localhost]
        -d --database <String>         : Database Name
        -c --connectionstring <String> : Connection String (Overrides --server and --database)
        -t --table <String>            : Table Name to Query
        -q --query <String>            : Custom SQL Query (Overrides --table)
        -F --format <String>           : Custom String.Format output
        -f --follow                    : Follow output as it gets added
        -s --sleep-interval <Integer>  : Sleep interval in ms [200]
        -r --retry                     : keep trying to query if it is unsuccessful
        -h --help                      : This help
