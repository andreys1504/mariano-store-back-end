# MarianoStore - back end
Demonstração de um sistema baseado em microsserviços, onde os serviços se comunicam através um Message Broker. 

![Diagrama de Eventos](https://ik.imagekit.io/ryeaswait/MarianoStoreDiagramaEventsBroker.jpg)

#### Command e Event

Command, requisita que uma ação seja realizada: Cadastrar, buscar, alterar, etc.
<br />
Event, é o resultado de uma ação que alterou o estado do sistema: Cadastro realizado, Pagamento efetuado, Cancelamento realizado.

### Frameworks/Libs
- .NET/C#
- RabbitMQ
- MediatR
