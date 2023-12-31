# MarianoStore - back end
Demonstração de um sistema baseado em microsserviços, onde os serviços se comunicam através um Message Broker: Event Driven Architecture. 

![Diagrama de Eventos](https://ik.imagekit.io/ryeaswait/FluxoEventos.png)

#### Camadas

Obs.: Em um cenário real, cada serviço deve estar em uma solution: Catalogo, Notificacoes, Pagamento e Pedidos. 
<br />
Obs. 2: MarianoStore.Core, MarianoStore.Infra.Services e MarianoStore.Ioc, são libs base para cada Serviço e, num cenário real, preferencialmente, devem ser gerados pacotes NuGet.

- SERVICO.Application, implementações de funcionalidades a serem consumidas por camadas superiores: API, Job/Workers, etc. Orquestra o fluxo de uma funcionalidade: chamada a repositories, entities, domain services, disparo de events, persistência na base, etc.
- SERVICO.Domain, implementa entities, events, domain services, repositories (abstrações), tipos bases que representam as regras de negócio de um sistema.

#### Commands e Events

- Command, requisita que uma ação seja realizada: Cadastrar, buscar, alterar, etc.
- Event, é o resultado de uma ação que alterou o estado do sistema: Cadastro realizado, Pagamento efetuado, Cancelamento realizado.

### Frameworks/Libs

- .NET/C#
- ASP.NET
- RabbitMQ
- MediatR
- Docker
- SQL Server

### Execução ambiente Local

-  Executar:<br />
docker volume create --driver local --opt type=none --opt device=PATH --opt o=bind mariano-store--sql_data
docker volume create --driver local --opt type=none --opt device=PATH --opt o=bind mariano-store--sql_log
-  Executar: docker-compose up -d
-  Criar as base junto ao SQL Server: Catalogo_MarianoStore, Notificacoes_MarianoStore, Pagamento_MarianoStore e Pedidos_MarianoStore
-  Criar tabelas base. Ver: ./sql-server/TabelasBase.sql
