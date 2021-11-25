FROM openjdk:8
EXPOSE 8080
Add target/docker-shoppingapi.jar docker-shoppingapi.jar
ENTRYPOINT ["java","-jar","/docker-shoppingapi.jar"]
