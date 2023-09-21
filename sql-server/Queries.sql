
/*
DELETE FROM [Pedidos_MarianoStore].dbo.MessageInBroker
DELETE FROM [Pagamento_MarianoStore].dbo.MessageInBroker
DELETE FROM [Notificacoes_MarianoStore].dbo.MessageInBroker
DELETE FROM [Catalogo_MarianoStore].dbo.MessageInBroker
*/

SELECT * FROM [Pedidos_MarianoStore].dbo.MessageInBroker WHERE OriginalContext IS NULL AND IsEvent = '1'
SELECT * FROM [Pagamento_MarianoStore].dbo.MessageInBroker WHERE IsEvent = '1' --AND OriginalContext IS NULL 
SELECT * FROM [Notificacoes_MarianoStore].dbo.MessageInBroker --WHERE OriginalContext IS NULL
--SELECT * FROM [Catalogo_MarianoStore].dbo.MessageInBroker


/*
SELECT * FROM [Pedidos_MarianoStore].dbo.MessageInBroker 
WHERE 
	MessageInBroker IS NULL
	AND [Stored] < DATEADD(second, -3, GETUTCDATE()) 
	    15:10:00 < 15:10:03*/