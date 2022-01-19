Funcionalidade: 04_CenariosCalculoConversaoComTaxaParametrizada
Teste de cálculo de conversão de moéda estrangeira para Real utilizando taxas parametrizadas por segmento de cliente.

Contexto: Possibilidade de consultar as taxas cadastradas por segmento de cliente
	Dadas as seguintes configurações de taxas já cadastradas no sistema
		| Id                                   | Rate | Segment      |
		| ecd6f401-751d-42b8-9528-82277b84e0f3 | 0,1  | Varejo       |
		| 095c5d65-d232-4c2c-b01b-88944b225bb3 | 0,05 | Personnalite |
		| 7a3c9372-0064-4f06-8276-e71ac87d700b | 0,01 | Private      |

Esquema do Cenário: 0401 - Calcular o valor total a ser pago pelo cliente
	Quando o usuário solicitar o cálculo de conversão de 1 "EUR" para Real para o segmento "<Segmento>"
	Então deve ser retornado o valor <ValorTotal>

	Exemplos:
		| Segmento     | ValorTotal |
		| Varejo       | 1,10       |
		| Personnalite | 1,05       |
		| Private      | 1,01       |