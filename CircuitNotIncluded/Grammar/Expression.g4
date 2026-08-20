grammar Expression;

fragment DIGIT: [0-9];
fragment CHAR: [a-zA-Z];

TRUE: 'true';
FALSE: 'false';
BUFFER: 'buffer';
FILTER: 'filter';
NOT: '!';
AND: '*';
XOR: '#';
OR: '+';
LPAREN: '(';
RPAREN: ')';
COMMA: ',';
FLOAT: DIGIT+ ('.' DIGIT+)?;
ID: (CHAR | '_') (CHAR | DIGIT | '_')*;

WS: [ \t]+ -> skip;
ERROR : . ;

// Parser rules
program: expression? EOF;

expression
    : factor                        #factorExpresssion
    | NOT factor                    #notExpresssion
    | expression AND expression     #andExpresssion
    | expression XOR expression     #xorExpresssion
    | expression OR expression      #orExpresssion
    ;

factor
    : ID                            #idFactor
    | TRUE                          #trueFactor
    | FALSE                         #falseFactor
    | LPAREN expression RPAREN      #parFactor
    | function                      #funcFactor
    ;
    
function
    : BUFFER LPAREN expression COMMA FLOAT RPAREN  #bufferFunction
    | FILTER LPAREN expression COMMA FLOAT RPAREN  #filterFunction 
    ;