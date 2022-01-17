Funcionalidade: 01_CenariosDeInsercaoDeDados
Teste simples para validar os métodos de INSERÇÃO das taxas parametrizadas por segmento de cliente.

Cenário: 0101 - Deve inserir uma nova taxa customizada para um determinado segmento de clientes
	Dadas as seguintes configurações de taxas para serem cadastradas
		| Rate | Segment      |
		| 10   | Varejo       |
		| 20   | Personnalite |
		| 30   | Private      |
	Quando o usuário solicitar a inclusão das taxas
	Então deve conter o seguinte registro na tabela SegmentRate
		| Rate | Segment      |
		| 10   | Varejo       |
		| 20   | Personnalite |
		| 30   | Private      |