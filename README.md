# facebook-ip-resolver
IP address resolver for facebook while your dns is a lier

## Why this exist?
This tool help you access facebook if DNS server from Internet Provider decide to lie to you.
What it does is give you a fake IP for facebook domain, which lead user unable to access facebook website.

## How can this help
This tool will ask google's dns for correct facebook ip list and append the IP list to `host` file,
so, you can access facebook.com directly without changing your dns server.

## So why don't we use google's dns in the first place?
Because the user don't know how to change their dns server in the first place.
More over, the delay between user's computer and google's dns server might a problem, which can slow down the web surfing speed.

## how about looking for some host file in the internet?
because it might already outdate ... and why we have to do it manually?

## How this work?
Step 1: Backup dns config from registry
Step 2: Change default dns server to google's dns
Step 3: Find facebook's IP addresses
Step 4: Remove everything related to facebook in host file
Step 5: Append all facebook IPs from Step 3
Step 6: restore the original dns server
