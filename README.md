# simcom_a7670sa_dotnet

Para instanciar um objeto responsável pela comunicação, você deverá informar ao menos a porta COM do módulo e o BaudRate. Os valores são os mesmos utilizados no assistente SSCOM32E.exe. Por padrão, o valor do BaudRate é 115200.

```csharp
var simModule = new SimcomA7670SA("COM7", 115200);
```

#### Enviar SMS ####

O envio de SMS é muito simples. Basta acionar o método *SendSMS(phoneNumber, message)*, conforme exemplo a seguir:

```csharp
string result = simModule.SendSMS("44999991234", "TESTANDO ENVIO DE SMS EM CSHARP!");
```

O resultado do comando é uma string contendo o status *OK* ou a mensagem "ERROR" juntamente com o motivo do insucesso do envio.

#### Listar SMS ####

A listagem de SMS acontece por intermédio do método *ListSMS(listSmsType)*, que deve ser informado o tipo de listagem desejada. O retorno será uma string contendo todas as mensagens da listagem.

```csharp
string result = simModule.ListSMS(SimcomA7670SASmsReadEnum.All);
```

#### Ler SMS ####

Por fim, para realizar a leitura de um SMS, marcando a mensagem como lida, será necessário informar o índice da mensagem para o método *ReadSMS(index)*. O índice pode ser obtido através da listagem de SMS não lidos, sendo o valor que sucede o prefixo *"+CMGL: "* de cada mensagem. Por exemplo:

```
AT+CMGL="REC UNREAD"
+CMGL: 19,"REC UNREAD","8462","","24/11/17,15:50:00+92"
Alex, OPORTUNIDADE UNICA: TIM CONTROLE com 27GB, ligacoes, WhatsApp e Messenger ilimitados por 29,99/mes! controletim.com.br/ATUMSGX

OK
```

Sendo assim, podemo executar o método *ReadSMS(index)* passando o valor *19* como argumento para o parâmetro *index*. O resultado será uma string contendo a estrutura do SMS, porém agora com o status "REC READ":

```csharp
string result = simModule.ReadSMS(19);
```