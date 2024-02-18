# Yadebs
Yet another double entry bookkeeping system 
It should at least get some accounts and a ledger

Has an option for double entry bookkeeping  and a different one for income surplus c

Set a local secret to connect to database with

dotnet user-secrets set ConnectionStrings:AccountingContext "Server=localhost;Port=5432;Database=yadebs;Username=xxx;Password=xxx"


ef core migration

in root folder

dotnet ef migrations add --project Yadebs.Db\Yadebs.Db.csproj --startup-project Yadebs.Api\Yadebs.Api.csproj --context Yadebs.Db.AccountingContext  --verbose IncomeSurplus --output-dir Migrations


dotnet ef database update --project Yadebs.Db\Yadebs.Db.csproj --startup-project Yadebs.Api\Yadebs.Api.csproj --context Yadebs.Db.AccountingContext 