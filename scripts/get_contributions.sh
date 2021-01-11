#!/bin/bash

# Use like this:
# ./scripts/get_contributions.sh perlun 2020

USER=${1?user missing}
YEAR=${2?year missing}

curl -s "https://github.com/users/${USER}/contributions?from=${YEAR}-01-01&to=${YEAR}-12-31" | xmlstarlet fo -R | xmlstarlet sel -t -c '(//svg)[1]'
