INSERT INTO [Jedis] ([Id], [Nom], [IsSith], [Image]) VALUES 
(1, 'Obi Wan Kenobi', 0, 'obiwan.png'),
(2, 'Yoda', 0, 'yoda.png'),
(3, 'Mace Windu', 0, 'windu.png'),
(4, 'Aayla Secura', 0, 'secura.png'),
(5, 'Shaak Ti', 0, 'shaakti.png'),
(6, 'Plo Koon', 0, 'plokoon.png'),
(7, 'Kit Fist', 0, 'kitfist.png'),
(8, 'Qui Gon Jinn', 0, 'quigon.png'),
(9, 'Luke Skywalker', 0, 'luke.png'),
(10, 'Anakin Skywalker', 1, 'anakin.png'),
(11, 'Rey Skywalker', 0, 'rey.png'),
(12, 'Dark Sidious', 1, 'sidious.png'),
(13, 'Dark Vador', 1, 'vador.png'),
(14, 'Grivious', 1, 'grivious.png'),
(15, 'Dark Maul', 1, 'darkmaul.png'),
(16, 'Dooku', 1, 'dooku.png'),
(17, 'Kylo Ren', 1, 'kylo.png'),
(18, 'Dark Revan', 1, 'revan.png')

INSERT INTO [Stades] ([Id], [Nom], [NbPlaces], [Image]) VALUES 
(1, 'Coruscant', 1000000,'planet.png'),
(2, 'Kamino',100000,'planet.png'),
(3, 'Naboo',10000,'planet.png'),
(4, 'Tatooine',666,'planet.png'),
(5, 'Dagobah',4,'planet.png')

INSERT INTO [StadeCarac] ([IdStade], [IdCarac]) VALUES
(1, 5), (1, 20), (1, 22),
(2, 22), (2, 6),
(3, 19), (3, 21),
(4, 19), (4, 18),
(5, 19), (5, 20)

INSERT INTO [Caracteristiques] ([Id], [Nom], [Def], [Type], [Valeur]) VALUES
(1, 'Le réveil de la force', 'Perception', 'Jedi', 30),
(2, 'Esprit obscurci', 'Perception', 'Jedi', 40),
(3, 'The chosen one', 'Perception', 'Jedi', 70),
(4, 'Je suis un Skywalker', 'Perception', 'Jedi', 30),
(5, 'Temple Jedi', 'Perception', 'Stade', 20),
(6, 'Sanctuaire Sith', 'Perception', 'Stade', -20),
(7, 'Padawan', 'Perception', 'Jedi', 10),
(8, 'Apprenti Sith', 'Perception', 'Jedi', 15),
(9, 'Guidé par un esprit', 'Perception', 'Jedi', 20),
(10, 'Grand Maître Jedi', 'Perception', 'Jedi', 50),
(11, 'Seigneur Sith', 'Perception', 'Jedi', 50),
(12, 'Combat au sabre laser', 'Strength', 'Jedi', 30),
(13, 'Double sabre', 'Strength', 'Jedi', 50),
(14, 'Sabre double lame', 'Strength', 'Jedi', 40),
(15, 'Eclairs', 'Strength', 'Jedi', 50),
(16, 'Etranglement', 'Strength', 'Jedi', 50),
(17, 'Projection', 'Strength', 'Jedi', 30),
(18, 'Arène de gladiateur', 'Strength', 'Stade', 20),
(19, 'Combat dans la nature', 'Strength', 'Stade', 40),
(20, 'Utilisation du décor', 'Strength', 'Stade', -40),
(21, 'Public à utiliser', 'Strength', 'Stade', -20),
(22, 'Soutien Stormtroopers', 'Strength', 'Stade', -30),
(23, 'Parade au sabre laser', 'Dexterity', 'Jedi', 40),
(24, 'Armure légère', 'Dexterity', 'Jedi', 20),
(25, 'Garde', 'Dexterity', 'Jedi', 10),
(26, 'Unité R2', 'Dexterity', 'Jedi', 5),
(27, 'Je suis un wookie', 'Dexterity', 'Jedi', 60),
(28, 'Je suis un androïd', 'Dexterity', 'Jedi', 70),
(29, 'Le dessus sur la lave', 'Dexterity', 'Stade', 40),
(30, 'Complexe de destruction massif', 'Dexterity', 'Stade', -40)

INSERT INTO [JediCarac] ([IdJedi], [IdCarac]) VALUES
(1, 10), (1, 12), (1, 23), (1, 9), (1, 24),
(2, 10), (2, 12), (2, 23), (2, 9),
(3, 10), (3, 12), (3, 23), (3, 24),
(4, 10), (4, 12), (4, 23), (4, 24),
(5, 10), (5, 12), (5, 23), (5, 24),
(6, 10), (6, 12), (6, 23), (6, 24),
(7, 10), (7, 12), (7, 23), (7, 24),
(8, 10), (8, 12), (8, 23), (8, 24),
(9, 10), (9, 4), (9, 23), (9, 9),
(10, 2), (10, 3), (10, 13), (10, 23),
(11, 1), (11, 4), (11, 12), (11, 23),
(12, 11), (12, 15), (12, 25), (12, 23),
(13, 11), (13, 3), (13, 16), (13, 28), (13, 23),
(14, 11), (14, 12), (14, 28), (14, 23),
(15, 8), (15, 14), (15, 23),
(16, 11), (16, 12), (16, 23),
(17, 2), (17, 12), (17, 23),
(18, 8),  (18, 12), (18, 23)

INSERT INTO [Matches] ([Id], [Jedi1], [Jedi2], [Stade], [Vainqueur], [Phase]) VALUES
(1, 1, 2, 1, 0, 14),
(2, 3, 4, 2, 0, 13),
(3, 5, 6, 3, 0, 12),
(4, 7, 8, 4, 0, 11),
(5, 9, 10, 1, 0, 10),
(6, 11, 12, 2, 0, 9),
(7, 13, 14, 3, 0, 8),
(8, 15, 16, 4, 0, 7),
(9, 0, 0, 1, 0, 6),
(10, 0, 0, 2, 0, 5),
(11, 0, 0, 3, 0, 4),
(12, 0, 0, 4, 0, 3),
(13, 0, 0, 1, 0, 2),
(14, 0, 0, 3, 0, 1),
(15, 0, 0, 2, 0, 0)

INSERT INTO [MatchTournoi] ([IdTournoi], [IdMatch]) VALUES
(1, 1),
(1, 2),
(1, 3),
(1, 4),
(1, 5),
(1, 6),
(1, 7),
(1, 8),
(1, 9),
(1, 10),
(1, 11),
(1, 12),
(1, 13),
(1, 14),
(1, 15)

INSERT INTO [Users] ([Id], [Login], [Password], [Prenom], [Nom]) VALUES
(1, 'begarco', '15851918021222115514974461602051215898129243254222', 'Benoît', 'Garçon')

INSERT INTO [Tournois] ([Id], [Nom]) VALUES
(1, 'Mos Eisley Tournament')

