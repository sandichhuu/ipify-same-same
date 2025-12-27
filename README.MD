#ipify-same-same

This project is public ip provider written in ASP.NET 10.
Target: Deploy to linux, use AOT for fast, small image after load into server.

Support endpoint: /v4, /v6, default is return IPv4.

Why we need this ?
If you have too weak VPS server or cloud, you will need this.
After setup DNS record to route the address into in-house PC without static IP
We need a free, unlimit DDNS updater like ddclient or ddns-updater, ...
But the service provide the public IP, the most important data ddclient needs to modify the DNS record is alway limit.
Install this docker image to your linux, weak, low power server, only consume 12MB on your drive after docker load.
Then you have free one IP v4/v6 serving like IPIFY.

Have no server ? 
Chat with me, I can share you some without any fee.
But please don't spam the API.

Test:
![Test case v4/default]('test.png')
