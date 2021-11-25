FROM openjdk:8
EXPOSE 8080
ADD target/shoppingapi.jar shoppingapi.jar
ENTRYPOINT ["java","-jar","/shoppingapi.jar"]