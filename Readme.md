
## Configurações
### Altere o arquivo 'local.settings.json', ajuste a informação:
- "ServiceBusConnString": "conexão com seu bus"

### Altere o arquivo 'FunctionBusTrigger.cs', ajustando as informações conforme item abaixo:
- [ServiceBusTrigger("nome da sua queue", Connection = "nome da chave configurada acima")] ex: ServiceBusConnString
