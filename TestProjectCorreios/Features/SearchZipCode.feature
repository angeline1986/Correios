Feature: Correios Search
	Zip code search

	@mytag
	Scenario: Zip code search
		Given I access the CEP Search on the Correios website
		When I enter zip code I get address
		  | zip code | street                               |
		  | 80700000 | Dados não encontrado                 |
		  | 01013001 | Rua Quinze de Novembro, São Paulo/SP |
		Then I send an order "SS987654321BR"