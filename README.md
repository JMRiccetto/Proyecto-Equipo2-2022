Desafíos: 

Los desafíos que nos parecieron más difíciles en general fue como pensar el flujo del programa pensando en que a futuro íbamos a tener que utilizar el bot de telegram ya que es algo con lo que no estamos muy familiarizados.  

Un ejemplo de esto es que nos costó entender que cuando uno de los dos jugadores envié un comando, como hacer que esta acción no repercuta en la consola del otro jugador, por ejemplo, si un jugador quiere visualizar su tablero, como hacer que el bot sepa cuál de los dos jugadores es el que le envió el comando, y en base a eso se imprima el tablero solamente en la consola del jugador que envió el comando y no en la del otro ya que sino el otro jugador vería donde están posicionados los barcos del contrincante. 

Otro de los desafíos que nos resultó difícil fue la implementación de los Handlers ya que en un principio teníamos una clase MatchLogic la cual tenía varias responsabilidades y nos costó abstraer que varias de las responsabilidades de esa clase tenían que ser implementadas por Handlers. 

Tuvimos que cambiar algunas de las funcionalidades que nos habiamos propuesto realizar al principio como la de jugar contra el bot, ya que vimos que no nos iba
a dar el tiempo para implementar esta funcionalidad y decidimos reemplazarla y priorizar otras cosas del proyecto.

Otro de los desafíos que nos resulto difícil al principio es como enviar desde un handler un mensaje a los dos jugadores hasta que descubrimos el metodo
sendTextMessageAsync del Telegram bot. 

También como hacer que el usuario pueda enviar un mensaje como por ejemplo una coordenada y que el handler la lea sin tener que hacer un comando para cada 
coordenada. Para esto aprovechamos el polimorfismo de la estructura de base handler y cambiamos el metodo CanHandle en cada Handler en particular.


Aprendizajes: 

Algo que aprendimos que nos pareció útil es a como implementar tests unitarios de métodos que retornen una excepción esperada. 

También nos familiarizamos más con el git ya que en un principio tuvimos varios problemas con el primer repo y no tuvimos más opción que crear un repo nuevo. 

Mejoramos en la utilizar de GIT ya que en un principio tuvimos problemas con los conflictos del repositorio, cosa que nos llevó a profundizar mejor en el tema.

Otra de las cosas nos pareció bastante util es el principio Change of Responsabilities que nos sirve para poder programar otro bot a futuro.


Comentarios: 
	
En cuanto al proyecto en general nos pareció bastante entretenido e interesante. La dificultad fue acorde al curso aunque la única crítica es que sobre todo
en la última entrega estuvimos más de la mitad del tiempo viendo como hacer que el bot funcione correctamente y esto no nos permitió poder aplicar más y mejor
los principios y patrones dados en clase.

En cuanto a nuestro proyecto en particular, vimos posibles mejoras sobre la entrega que decidimos no cambiarlas por temas de tiempo. Por ejemplo, en vez de
tener un atributo para cada configuración de la partida en GameUser como ¨bomas" o "gameboardSide" podríamos haber creado una clase Menu que agregue todas
estas configuraciones y que el usuario solamente tenga un atributo del tipo Menu. Esto nos permitiria que al momento de agregar otra configuración, solamente
agregarla en Menu y que la clase GameUser tenga acoplamiento tan alto.