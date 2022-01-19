Funcionalidade: 05_CenariosCalculoConversaoSemTaxaParametrizada
Teste de cálculo de conversão de moéda estrangeira para Real SEM utilização de taxas parametrizadas por segmento de cliente.

Esquema do Cenário: 0501 - Calcular o valor total a ser pago pelo cliente
	Quando o usuário solicitar o cálculo de conversão de 12 "EUR" para Real para o segmento "<Segmento>"
	Então deve ser retornado o valor <ValorTotal>

	Exemplos:
		| Segmento     | ValorTotal |
		| Varejo       | 12         |
		| Personnalite | 12         |
		| Private      | 12         |