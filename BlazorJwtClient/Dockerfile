# =========================
# BUILD
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app/publish

# =========================
# RUNTIME (NGINX)
# =========================
FROM nginx:alpine

# rimuove config default
RUN rm /etc/nginx/conf.d/default.conf

# config per SPA (Blazor WASM)
COPY nginx.conf /etc/nginx/conf.d/default.conf

# copia file statici
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
