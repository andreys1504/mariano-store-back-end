# MarianoStore - back end
Demonstração de um sistema baseado em microsserviços, onde os serviços se comunicam através um Message Broker. 

![Diagrama de Eventos](https://ik.imagekit.io/ryeaswait/MarianoStoreDiagramaEventsBroker.jpg)

#### Camadas

Obs.: Em um cenário real, cada serviço deve estar em uma solution: Catalogo, Notificacoes, Pagamento e Pedidos. 
Obs. 2: MarianoStore.Core, MarianoStore.Infra.Services e MarianoStore.Ioc, são libs base para cada Serviço e, num cenário real, devem ser gerados pacotes NuGet.

- SERVICO.Application, implementações de funcionalidades a serem consumidas por camadas superiores: API, Job/Workers, etc. Orquestra o fluxo de uma funcionalidade: chamada a repositories, entidades, disparo de events, domain services, persistência na base.
- SERVICO.Domain, implementa entities, events, domain services, repositories (abstrações), tipos bases que representam as regras de negócio de um sistema.

#### Command e Event

- Command, requisita que uma ação seja realizada: Cadastrar, buscar, alterar, etc.
- Event, é o resultado de uma ação que alterou o estado do sistema: Cadastro realizado, Pagamento efetuado, Cancelamento realizado.

### Frameworks/Libs
- .NET/C#
- RabbitMQ
- MediatR
