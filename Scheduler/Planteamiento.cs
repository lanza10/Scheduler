namespace Scheduler
{
    public class Planteamiento
    {
        /* 
         
          En el problema diferenciamos dos casos de una manera clara cuya ramificación dependera del atributo Type:
         
            -Type == Once : 
                    En este caso, la fecha que devolverá output será la establecida en el campo DateTime de la sección Configuration (FECHA Y HORA!!!) y sacar 
                    también la descripcion de esta fecha junto con la establecida como StartDate en la seccion Limits.

                    DATOS DE ENTRADA RELEVANTES -> DateTime | StartDate 

                    CAMPOS DE OUTPUT : 
                    - NextExecTime == DateTime (sección Configuration) 
                    - Description == $"Occurs once.Schedule will be used on {NextExecTime} starting on {StartDate}"

            -Type == Recurring
                    En este caso, deberá devolver la fecha CurrentDate + los días establecidos en Days. De nuevo el límite se tendra en cuenta para la descripción
                    así como el campo Occurs para establecer en esta cada cuanto ocurre.
                    (SE ASUME QUE EN EL EJEMPLO LA FECHA USADA PARA LA DESCRIPCIÓN ES ERRÓNEA Y DEBERÍA SER LA MISMA QUE DEVUELVE OUTPUT)

                    DATOS DE ENTRADA RELEVANTES -> CurrentDate | Days | StartDate | Occurs

                    CAMPOS DE OUTPUT : 
                    - NextExecTime == CurrentDate + Days 
                    - Description == $"Occurs every (day/week/month...).Schedule will be used on {NextExecTime} starting on {StartDate}"
    
            ESTRUCTURA: 
                
              Planeo crear un modelo para cada seccion, cada uno con los diferentes atributos y constructor. Posteriormente crear los diferentes validadores
              para cada modelo. Planteo crear un modelo compuesto de los tres modelos Input, Configuration y Limits denominado SchedulerInput 
              a partir del cual mediante su capa servicio calcule el output. La idea de este planteamiento es pensar en una futura expansion de las diferentes 
              secciones y así poder crear un código más mantenible evitando clases enormes.
              
              Actualmente me planteo el uso de Interfaces a modo de poder ofrecer mayor flexibilidad a la hora poder introduccir nuevas secciones o cambios.
            
                    
         */
    }
}
