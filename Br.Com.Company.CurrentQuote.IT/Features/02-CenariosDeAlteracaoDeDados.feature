Funcionalidade: 02_CenariosDeAlteracaoDeDados
Teste simples para validar os métodos de ATUALIZAÇÃO E DELEÇÃO das taxas parametrizadas por segmento de cliente.

Contexto: Possibilidade de alterar dados
	Dadas as seguintes configurações de taxas já cadastradas no sistema
		| Id                                   | Rate | Segment      |
		| ecd6f401-751d-42b8-9528-82277b84e0f3 | 10   | Varejo       |
		| 095c5d65-d232-4c2c-b01b-88944b225bb3 | 20   | Personnalite |
		| 7a3c9372-0064-4f06-8276-e71ac87d700b | 30   | Private      |

Cenário: 0201 - Deve alterar a taxa do segmento Varejo de clientes
	Quando o usuário alterar a taxa de "ecd6f401-751d-42b8-9528-82277b84e0f3" para 15
	Então deve conter o seguinte registro na tabela SegmentRate
		| Id                                   | Rate | Segment      |
		| ecd6f401-751d-42b8-9528-82277b84e0f3 | 15   | Varejo       |
		| 095c5d65-d232-4c2c-b01b-88944b225bb3 | 20   | Personnalite |
		| 7a3c9372-0064-4f06-8276-e71ac87d700b | 30   | Private      |

Cenário: 0201 - Deve deletar a taxa do segmento Private
	Quando o usuário deletar a taxa "7a3c9372-0064-4f06-8276-e71ac87d700b"
	Então deve conter o seguinte registro na tabela SegmentRate
		| Id                                   | Rate | Segment      |
		| ecd6f401-751d-42b8-9528-82277b84e0f3 | 10   | Varejo       |
		| 095c5d65-d232-4c2c-b01b-88944b225bb3 | 20   | Personnalite |