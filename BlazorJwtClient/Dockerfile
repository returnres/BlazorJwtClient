# ---------- BUILD ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .
RUN dotnet publish BlazorJwtClient/BlazorJwtClient.csproj -c Release -o /app/publish

# ---------- RUNTIME ----------
FROM nginx:alpine
WORKDIR /usr/share/nginx/html

# rimuove config default
RUN rm /etc/nginx/conf.d/default.conf

# config SPA
COPY nginx.conf /etc/nginx/conf.d/default.conf

# copia i file statici
COPY --from=build /app/publish/wwwroot .

EXPOSE 80
