use RamosDiversos
go

--DROP TABLE #TMP
select a.NUM_INSP,c.num_insp AS 'NUM_INSP_TAB_INSP',Insp_Obr,Ind_StatusInspecao,d.NUM_APOL_AN, b.NUM_PI, b.Ind_StatusInspecao
from Tab_Ped d 
inner join Tab_Ped_Loc a
	on a.NUM_PI = d.num_pi
inner join TAB_PED_LOC_COMPL b
	on b.NUM_PI = a.num_pi
	and b.NUM_ITEM = a.NUM_ITEM
inner join Tab_Trans_Insp c
	on c.NOSSO_NUMERO = b.NOSSO_NUMERO
	and c.NUM_ITEM = b.NUM_ITEM
where a.NOSSO_NUMERO like '021%'
and Insp_Obr = 1 order by 1 desc

select * from #TMP


select * from TAB_TRANS_COBERT_INSP where Num_Insp = 541545

select NUM_INSP,* from SPX21250PSQLNEO.YAS_ND.YAS.TAB_PED_LOC where num_apol = 1400462310 and NUM_ENDO = 0
select NUM_PI,NUM_PROP,* from SPX21250PSQLNEO.YAS_ND.YAS.TAB_CTRL_EMIS  where num_apol = 1400462310 and NUM_ENDO = 0
select * from SPX21250PSQLNEO.YAS_ND.YAS.TAB_PED_INSP  where NUM_PI = 2120015596



select NUM_INSP,* from SPX21250PSQLNEO.YAS_ND.YAS.TAB_PED_LOC where num_apol IN(
SELECT NUM_APOL_AN FROM #TMP 
)
and NUM_ENDO = 0 AND NUM_INSP > 0

SELECT * FROM INSPECAO..TAB_COBERT_INSP WHERE NUM_INSP = 541545

SELECT top 10 * FROM SPX21250PSQLNEO.YAS_ND.YAS.TAB_SAP_SIN_PAG  WHERE NUM_APOL IN(SELECT NUM_APOL_AN FROM #TMP)
SELECT top 10 * FROM SPX21250PSQLNEO.YAS_ND.YAS.TAB_SIN_PAG  WHERE NUM_APOL IN(SELECT NUM_APOL_AN FROM #TMP)

--Validar a inspeção da apolice anterior com os dados da proposta.

SELECT TOP 10 * FROM Inspecao..tab_insp