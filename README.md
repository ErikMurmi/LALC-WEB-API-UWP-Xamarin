
## Usuarios 
- email: user@gmail.com clave: 12345678
- email: udla@gmail.com clave: udla1234

# LALC-API-UWP
PROCEDIMIENTO PARA LA EJECUCIÓN DE LA SOLUCIÓN

a. Clonar el repositorio del proyecto LALC, de GitHub, que se va a vincular mediante el enlace en Visual Studio 2019. 
Existirán dos aplicativos: LALC-API y LALC-UWP. Así como también el archivo LALCDB.sql. Es indispensable el ejecutar 
este último en Visual Studio 2019 o en Microsoft SQL Server Management Studio 18, ya que contiene el código para la 
creación de la base de datos.

b. Una vez realizado el paso anterior, se necesita abrir LALC-API y es necesaria la modificación del archivo 
Web.config. En este añadiremos líneas de código relacionadas con el servidor donde se creó la Base de datos 
(ejecución del script) en cada ordenador personal. Por ello, se añade un Connection String, modificando el 
nombre del servidor para lo mencionado anteriormente.

c. Paso siguiente, ejecutar la aplicación en un navegador y comprobar que se encuentre en correcto funcionamiento.
De manera que si al ingresar la siguiente URL: https://localhost:44318/API/Categorias (sabiendo que se puede alternar
entre los nombres de los controladores), debe dirigir a los datos almacenados de Categorías de la Base de datos en formato Json.
Una vez verificado este paso, es necesario que la API se mantenga en ejecución.

d. Como siguiente punto, abrir, en una nueva pestaña de Visual Studio, el aplicativo LALC-UWP. El único paso a seguir 
es ejecutar en el ordenador y esperar a que la aplicación inicie, mostrando la pantalla para el inicio de sesión.

e. Terminado el paso anterior, ya se puede empezar a navegar por el aplicativo una vez hecho el Login. 
De otra manera, podrá registrarse para que exista exclusividad con respecto a los elementos que se guardarán por cada usuario.

f. Como página principal se encuentran la pantalla de categorías. Aquí se puede crear, editar, eliminar y
acceder a cada una de ellas. Al ingresar a una, podemos visualizar las subcategorías existentes, de las cuales
también se puede aplicar las acciones antes mencionadas para las categorías, así también los conceptos y, por último, 
las prácticas. De estas únicamente se podrá visualizar y eliminar a conveniencia del usuario.

g. Para una navegación más sencilla para el usuario, se añadieron 2 elementos en la pantalla de categoría 
y subcategoría: una barra lateral en la cual mostrará en lista todos los elementos referentes al mismo, 
y una sección para elementos prioritarios.
