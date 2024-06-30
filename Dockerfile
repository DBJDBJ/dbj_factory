# Start from .NET 8 SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0

# 2024Q3 node latest is 20. AFTER this is done from bash install Node.js and npm
# apt-get update && apt-get install -y curl \
# && curl -sL https://deb.nodesource.com/setup_20.x | bash - \
# && apt-get install -y nodejs \
# && apt-get clean

# Start bash shell by default
CMD ["bash"]
