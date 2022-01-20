Funcionalidade: 05_CenariosCalculoConversaoSemTaxaParametrizada
Teste de cálculo de conversão de moéda estrangeira para Real SEM utilização de taxas parametrizadas por segmento de cliente.

Esquema do Cenário: 0501 - Calcular o valor total a ser pago pelo cliente
	Dada taxa atual de conversão de EUR para Real no valor de 2,5
	Quando o usuário solicitar o cálculo de conversão de 12,1 "EUR" para Real para o segmento "<Segmento>"
	Então deve ser retornado o valor <ValorTotal>

	Exemplos:
		| Segmento     | ValorTotal |
		| Varejo       | 30,25      |
		| Personnalite | 30,25      |
		| Private      | 30,25      |