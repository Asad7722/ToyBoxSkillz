﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BR : MonoBehaviour
{


    public static List<string> listLine = new List<string>() {
"caixa de estrelas",
"caixa de nível",
"caixa de pontuação",
"Nível de alcance ",
"desafio de nível",
"desafio estrela",
"Assista a anúncios de vídeo",
"jogada aleatória",
"Toque",
"remover propagandas",
"vida ilimitada",
"assista ao vídeo e ganhe gemas grátis",
"vida",
"Pacotes",
"gemas",
"venda",
"mega box",
"caixa de iniciante",
"caixa mestra",
"super box",
"bem vindo ao nosso mundo doce",
"em breve...",
"começar",
"desafio",
"para ativação",
"para",
"coleção de brinquedos abertos",
"tempo limitado",
"colete estrelas para pegar a caixa de brinquedos de estrelas!",
"brinquedos novos:",
"giro livre",
"coletar pontuação para obter a caixa de brinquedos pontuação!",
"livre",
"alcance nível 100",
"alcance nível 70",
"alcance nível 85",
"alcance nível 130",
"alcance nível 222",
"alcance nível 250",
"alcance nível 200",
"alcance nível 155",
"alcance nível 185",
"leaderboards",
"coleção de brinquedos",
"alcance nível 10",
"alcance nível 30",
"alcance nível 55",
"Ponto: ",
"Estrela",
"nível ",
"conquistas",
"colecionar 600 estrelas",
"colecionar 900 estrelas",
"coletar 200000 pontos",
"alcance nível 270",
"coletar 300 estrelas",
"coletar 400000 pontos",
"coletar 600000 pontos",
"Melhor valor",
"separador de coluna",
"disjuntor de cor",
"quebra-linha",
"restaurar compras",
"bola liquidificadora",
"+5 movimentos",
"boosters box",
"bola de arco-íris",
"disjuntor único",
"bombear",
"ganhe presentes!",
"mais popular",
"destruir o vizinho da mesma cor",
"x disjuntor",
"foguete",
"colecionar brinquedos!",
"complete os objetivos.",
"bolha",
"chocolate",
"lock",
"gelo",
"bola",
"objetivo",
"alvo",
"faça um",
"clique e use",
"oi! nosso nome é maysa e kaan. Toque 2 ou mais cubos adjacentes do mesmo para esmagar e completar as metas",
"e use-o.",
"enigma",
"quadra",
"arena",
"mais 5 movimentos no começo",
"crie uma bola de arco-íris no começo",
"crie uma bomba mágica no começo",
"vida útil ilimitada + remover anúncios + caixa mestre",
"super box 40% off",
"1200 gemas 50% de desconto",
"600 gemas 40% de desconto",
"você ganhou",
"baixar e ganhar",
"perto",
"recuperação",
"pausado",
"caixa de presente",
"cúbico",
"requer nível 10",
"requer nível 20",
"exigir nível 23",
"bloqueado",
"nível falhado",
"não há mais movimentos!",
"fazer compras",
"você gosta de nós? você nos daria 5 estrelas?",
"número de estrelas requerido:",
"suba na arena e ganhe muitos presentes!",
"mapa",
"compre 10",
"compre 3",
"começar boosters",
"roda de presente",
"colete estrelas para ganhar a caixa de presente",
"presentes diários",
"complete 5 níveis sem perder e ganhe a caixa de presente",
"oferta especial",
"não há acesso à internet. por favor, tente novamente.",
"grande oferta",
"girar",
"novos jogos",
"mais jogos",
"master pack",
"Saída",
"gem box",
"pular nível",
"caixa de presente de desafio nivelado",
"Mais",
"festa de brinquedo",
"nos avalie",
"sem conexão",
"selecionar presentes",
"move",
"move-se?",
"Ponto",
"para",
"nível restante:",
"arena lendária",
"arena",
"compre a caixa da gema para suas gemas coletadas!",
"o número de pedras que você precisa coletar para obter a caixa:",
"novo episódio desbloqueado",
"nova arena desbloqueada",
"Parabéns!",
"novo presente de brinquedo",
"esperando",
"submarino",
"bola de boliche",
"martelo",
"selecionar presentes",
"colecione brinquedos e ganhe gemas:",
"exigir nível",
"alcance o nível alvo",
"coletar 75 estrelas",
"metas",
"desbloquear ao nível",
"pronto",
"tempo restante ",
"grande venda",
"grande oferta",
"Assista um video",
"desafio caixa de presente",
"arena mode play",
"modo de episódio",
"nível de jogo",
"modo arena",
"aleatória",
"boosters",
"caçador de cores",
"rubic",
"cinco movimentos",
"presentes:",
"toque 2 ou mais cubos adjacentes do mesmo para esmagar. E complete as metas",
"quebrador de arco-íris",
"novos presentes de brinquedo!",
"Bomba grande",
"Open Treasure Box",
"Caixa de tesouro",
"Combine Boosters",
"Creat e Blast",
"Combinar e Blast",
"MISSÕES DIÁRIAS",
"Star Challenge Gift Box",
"Obrigado pela compra.",
"A compra falhou.",
"Vida livre",
 "Afirmação",
 "abrir caixa de presente 1",
 "sua caixa de presente está pronta para abrir",
 "ganhe 5 níveis!",
 "Caixa de presente nivelada",
 "Nível de alcance",
 "Presentes diários",
 "você perderá seu desafio de caixa grátis",
 "Toque em 2 ou mais cubos da mesma cor",
 "Colete os itens restantes para concluir as metas.",
 "Toque em 5 cubos da mesma cor para fazer uma BOMBA!",
 "Toque na bomba!",
 "Toque em 7 cubos da mesma cor para fazer um foguete!",
 "OK! Agora toque no ROCKET!",
 "Toque em 8 cubos da mesma cor para fazer um X BREAKER!",
 "Toque no X BREAKER!",
 "Toque em 9 cubos da mesma cor para fazer uma BOMBA GRANDE!",
 "Toque na BOMBA GRANDE!",
 "Colete os brinquedos",
 "Toque 10 cubos da mesma cor para fazer um COLOR HUNTER!",
 "Ótimo! Toque no COLOR HUNTER!",
 "Toque em boosters conectados e veja o que acontece!",
 "ÚNICO DISJUNTOR! Destrói um cubo!",
 "Toque nos cubos ao lado de um gelo para coletá-lo!",
 "DISJUNTOR ROW!  Destrói uma linha!",
 "DISJUNTOR DA COLUNA!  Destrua uma coluna!",
 "Toque nos cubos ao lado de uma bolha para coletá-la!",
 "Toque nos cubos ao lado de um chocolate para coletá-lo!",
 "DISJUNTOR DE CORES!  Destrói cubos da mesma cor que você escolhe!",
 "BOLA MISTURADORA! embaralha todos os cubos!",
 "Toque nos cubos da mesma cor ao lado de uma bola para coletar!",
 "Toque nos cubos da mesma cor ao lado de um item para coletar!",
 "Toque nos cubos da mesma cor ao lado de um BLOQUEIO para coletar!",
 "Iniciar desbloqueio!",
 "Qual caixa de presente você gostaria?",
 "Sua caixa de presente está pronta para abrir.",
 "Preparando uma caixa de presente ...!",
 "CAIXAS DE PRESENTE PRONTAS!",
 "Qual caixa de presente você gostaria?",
 "Desbloqueie no nível 7!",
 "Novo evento!",
 "Passe 3 níveis sem perder para escolher caixas de presente.",
 "Abrir!",
  "Star Challenge GiftBox",
  "Desafio 5to5",
  "Novo presente",
  "Coletar estrelas",
   "Coletar pontuação",
   "você perderá seus desafios!",
   "você perderá o seu Free Box Challenge!",
   "você tem certeza!",
   "você vai perder uma vida!",
   "Últimos 5 movimentos!",
   "Life Box is Full",
   "Obrigado por jogar.",
   "Aqui está o seu presente",
    "Desbloquear no nível",
    "Colete estrelas e pontuação para abrir as caixas de presente!",
    "Alcance o nível 20",
  "Open Star Box",
  "Abrir caixa de pontuação",
  "Abrir caixa de episódio",
  "Win 5TO5 Challenge",
  "Win Star Challenge",
  "Win Daily Mission Box",
  "Presente Diário Aberto",
  "Use Begin Big Bomb",
  "Use o disjuntor Begin X",
  "Use Begin Color Hunter",
  "Alcance o nível 50",
  "Colete estrelas",
  "Colete pontuações",
  "Usar disjuntor único",
  "Use Row Breaker",
  "Use Column Breaker",
  "Use Color Breaker",
  "Use Blender Ball",
  "Caixa gratuita aberta",
  "Alcance o nível 100",
  "Colete Cubos",
  "Explodir bombas enormes",
  "Criar grandes bombas",
  "Criar e explodir bombas",
  "Criar e usar foguetes",
  "Crie e use X Rockets",
  "Gire a roda do presente",
  "Comece o jogo com começar Boosters",
  "Comece o jogo com Reward Ads Bomb Rockets",
  "Níveis de vitória",
  "Watch Reward Ads",
  "Alcance o nível 150",
  "Alcance o nível 200",
  "Alcance o nível 250",
  "Alcance o nível 500",
  "Alcance o nível 750",
  "Alcance o nível 1000",
  "Alcance o nível 1250",
  "Alcance o nível 1500",
  "Alcance o nível 2000",
  "Alcance o nível 2500",
  "O número de movimentos para a esquerda",
  "Criar e explodir"

    };
}
