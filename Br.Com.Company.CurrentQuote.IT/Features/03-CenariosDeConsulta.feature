Funcionalidade: 03_CenariosDeConsulta
Teste simples para validar os métodos consulta de taxa por segmento de cliente.

Contexto: Possibilidade de consultar as taxas cadastradas por segmento de cliente
	Dadas as seguintes configurações de taxas já cadastradas no sistema
		| Id                                   | Rate | Segment      |
		| ecd6f401-751d-42b8-9528-82277b84e0f3 | 10   | Varejo       |
		| 095c5d65-d232-4c2c-b01b-88944b225bb3 | 20   | Personnalite |
		| 7a3c9372-0064-4f06-8276-e71ac87d700b | 30   | Private      |

Esquema do Cenário: 0301 - Consultar uma determinada taxa por Id
	Quando o usuário consultar a taxa pelo id "<Id>"
	Então deve retornar os seguintes registros
		| Id   | Rate   | Segment   |
		| <Id> | <Rate> | <Segment> |

	Exemplos:
		| Id                                   | Rate | Segment      |
		| ecd6f401-751d-42b8-9528-82277b84e0f3 | 10   | Varejo       |
		| 095c5d65-d232-4c2c-b01b-88944b225bb3 | 20   | Personnalite |
		| 7a3c9372-0064-4f06-8276-e71ac87d700b | 30   | Private      |

Esquema do Cenário: 0302 - Consultar uma determinada taxa por segmento de cliente
	Quando o usuário consultar a taxa pelo segmento "<Segmento>"
	Então deve retornar os seguintes registros
		| Id   | Rate   | Segment   |
		| <Id> | <Rate> | <Segment> |

	Exemplos:
		| Segmento     | Id                                   | Rate | Segment      |
		| Varejo       | ecd6f401-751d-42b8-9528-82277b84e0f3 | 10   | Varejo       |
		| Personnalite | 095c5d65-d232-4c2c-b01b-88944b225bb3 | 20   | Personnalite |
		| Private      | 7a3c9372-0064-4f06-8276-e71ac87d700b | 30   | Private      |

Esquema do Cenário: 0303 - Consultar o valor da taxa por segmento de cliente
	Quando o usuário consultar o valor da taxa pelo segmento "<Segmento>"
	Então deve ser retornado o valor <Taxa>

	Exemplos:
		| Segmento     | Taxa |
		| Varejo       | 10   |
		| Personnalite | 20   |
		| Private      | 30   |

Esquema do Cenário: 0303 - Consultar o valor da taxa por id de segmento de cliente
	Quando o usuário consultar o valor da taxa pelo id "<Id>"
	Então deve ser retornado o valor <Taxa>

	Exemplos:
		| Id                                   | Taxa |
		| ecd6f401-751d-42b8-9528-82277b84e0f3 | 10   |
		| 095c5d65-d232-4c2c-b01b-88944b225bb3 | 20   |
		| 7a3c9372-0064-4f06-8276-e71ac87d700b | 30   |