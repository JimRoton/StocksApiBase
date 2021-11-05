######################################################
# 
# 
# 
######################################################

# download the base stocks api
cd ~
mkdir Repos
cd Repos
git clone https://github.com/JimRoton/StocksApiBase.git

# test the api
cd StocksApiBase
dotnet run
curl https://localhost:5001/Stocks/MSFT

# copy files from base
cp Controllers ..\StocksApi\
cp Controllers\StocksController.cs ..\StocksApi\Controllers\
cp .gitignore ..\StocksApi\
cp appsettings.Development.json ..\StocksApi\
cp appsettings.json ..\StocksApi\
cp Program.cs ..\StocksApi\
cp Startup.cs ..\StocksApi\
cp Stock.cs ..\StocksApi\
cp StocksApi.csproj ..\StocksApi\

# push stocksapi
cd ..\StocksApi
git add --all
git commit -m "initial commit"
git push

# development
- Add Interfaces
- Add services
- Create Manager
- Wire up manager > services (startup)
- Test

- Add NewtonSoft.Json "dotnet add package Newtonsoft.Json"
