  --Origem dos dados na tabela Tab_Trans_Insp_Equipamentos para os Ramos (300,620,710) e Tab_Trans_Insp para os ramos (112,113,114)
  
   sql_oper = "insert into TAB_INSP ("
    
    sql_oper = sql_oper & "num_apol, num_endo, num_insp, cod_ramo, cod_mot_solic, cod_empr_vist, nom_prop, nom_cont, dsc_obs, tip_pess, num_cnpj_cpf, num_cep, tip_logr, nom_logr, dsc_compl, dsc_cid, dsc_bairro,"
    sql_oper = sql_oper & "uf, num_tel, cod_unid, cod_prod, cod_corr, cod_cobert1, is_cobert1, cod_cobert2, is_cobert2, cod_cobert3, is_cobert3, cod_cobert4, is_cobert4, cod_cobert5, is_cobert5, cod_cobert6, is_cobert6,"
    sql_oper = sql_oper & "cod_cobert7, is_cobert7, cod_cobert8, is_cobert8, cod_cobert9, is_cobert9, cod_cobert10, is_cobert10, val_insp, cod_status, cod_user_cad, dat_cad, hor_cad, cod_user_email, dat_env_email,"
    sql_oper = sql_oper & "hor_env_email, dat_ret_email, hor_ret_email, cod_user_derie, dat_derie, hor_derie, cod_parec_geral, dsc_parec_derie1, dsc_parec_liber2, num_syas, dat_insp, cod_ocup_seg, num_notsso_num,"
    sql_oper = sql_oper & "cod_dept_liber, cod_user_liber, dat_liber_insp, hor_liber_insp, cod_liber_emis, cod_clasf, cod_compl_clasf, dat_emis_apol_endo, num_insp_ant, e_mail_terc, anomes_pag_terc,"
    sql_oper = sql_oper & "Num_Cotacao, Num_Proposta, Dat_Receb, Num_Logr, cod_clasf_d, cod_compl_clasf_d, AtivDivergente, EndDivergente, Dsc_EndDiver, Tip_Cancel, dat_inspec, flg_confid, dat_agend,"
    sql_oper = sql_oper & "num_item, NUM_EQUIP, NOME_EQUIP, TIPO_EQUIP, CLASSE_EQUIP, ANO_FABRICACAO_EQUIP, MARCA_EQUIP, MODELO_EQUIP, NUM_SERIE_EQUIP, CHASSI_EQUIP"

    sql_oper = sql_oper & ") values ("
    sql_oper = sql_oper & Cdblx(PNumApol) & ","                  'Num_APOL
    sql_oper = sql_oper & PNumEndo & ","                         'Num_Endo
    sql_oper = sql_oper & Trim(LNumInsp) & ","                   'Num_Insp
    sql_oper = sql_oper & Trim(Mid(LblCodRamo, 1, 3)) & ","      'Cod_Ramo
    sql_oper = sql_oper & Mid(CmbTipInsp.Text, 1, 1) & ","       'Cod_Mot_Solic
    sql_oper = sql_oper & Trim(LblCodEmpr) & ",'"                'Cod_Empr_Vist
    sql_oper = sql_oper & Trim(TxtNomProp) & "','"               'Nom_Prop
    sql_oper = sql_oper & Trim(TxtContato) & "','"               'Nom_Cont
    sql_oper = sql_oper & Trim(TxtObs) & " ',"                   'Dsc_Obs
    sql_oper = sql_oper & Mid(CbmPes.Text, 1, 1) & ","           'Tip_Pess
    sql_oper = sql_oper & Trim(TxtReg) & ","                     'Num_Cnpj_Cpf
    sql_oper = sql_oper & Trim(TxtCep) & ",'"                    'Num_Cep
    sql_oper = sql_oper & Trim(TxtTipo) & "','"                  'Tip_Logr
    sql_oper = sql_oper & Trim(TxtLogr) & "','"                  'Nom_Logr
    sql_oper = sql_oper & Trim(TxtComl) & "','"                  'Dsc_Compl
    sql_oper = sql_oper & Replace(Trim(TxtCid), "'", "") & "','" 'Dsc_Cid
    sql_oper = sql_oper & Trim(TxtBairro) & "','"                'Dsc_Bairro
    sql_oper = sql_oper & Trim(TxtEst) & "','"                   'Uf
    sql_oper = sql_oper & Trim(TxtTel) & "',"                    'Num_Tel
    sql_oper = sql_oper & Mid(LblCodUnid.Caption, 1, 4) & ","    'cod_unid
    sql_oper = sql_oper & Mid(LblProd.Caption, 1, 4) & ","       'Cod_Prod
    sql_oper = sql_oper & Mid(lblCorrAbrv.Caption, 1, 7) & ","   'Cod_Corr   
    sql_oper = sql_oper & 0 & ","                          'Cod_Cobert1
    sql_oper = sql_oper & 0 & ","                          'Is_Cobert1
    sql_oper = sql_oper & 0 & ","                          'Cod_Cobert2
    sql_oper = sql_oper & 0 & ","                          'Is_Cobert2
    sql_oper = sql_oper & 0 & ","                          'Cod_Cobert3
    sql_oper = sql_oper & 0 & ","                          'Is_Cobert3
    sql_oper = sql_oper & 0 & ","                          'cod_Cobert4
    sql_oper = sql_oper & 0 & ","                          'Is_Cobert4
    sql_oper = sql_oper & 0 & ","                          'Cdo_Cobert5
    sql_oper = sql_oper & 0 & ","                          'Is_Cobert5
    sql_oper = sql_oper & 0 & ","                          'Cod_Cobert6
    sql_oper = sql_oper & 0 & ","                          'Is_Cobert6
    sql_oper = sql_oper & 0 & ","                          'Cod_Cobert7
    sql_oper = sql_oper & 0 & ","                          'Is_Cobert7
    sql_oper = sql_oper & 0 & ","                          'Cod_Cobert8
    sql_oper = sql_oper & 0 & ","                          'Is_Cobert8
    sql_oper = sql_oper & 0 & ","                          'Cod_Cobert9
    sql_oper = sql_oper & 0 & ","                          'Is_Cobert9
    sql_oper = sql_oper & 0 & ","                          'Cod_Cobert10
    sql_oper = sql_oper & 0 & ","                          'Is_Cobert10    
    sql_oper = sql_oper & 0 & ","     '
    sql_oper = sql_oper & Cdblx(LStatus) & ",'"                         'Cod_Status
    sql_oper = sql_oper & lUsuario & "',"                        'Cod_User_Cad
    sql_oper = sql_oper & Cdblx(LDatCad) & ","                          'Dat_Cad
    sql_oper = sql_oper & Cdblx(lhorcad) & ",'"                         'Hor_Cad
    sql_oper = sql_oper & LblBranco & "',"                       'Cod_User_Email
    sql_oper = sql_oper & 0 & ","                                'Dat_Ret_Email
    sql_oper = sql_oper & 0 & ","                                'Hor_Env_Email
    sql_oper = sql_oper & 0 & ","                                'Dat_Ret_Email
    sql_oper = sql_oper & 0 & ",'"                               'Hor_Ret_Email
    sql_oper = sql_oper & LblBranco & "',"                       'Cod_User_Derie
    sql_oper = sql_oper & 0 & ","                                'Dat_Derie
    sql_oper = sql_oper & 0 & ","                                'Hor_Derie
    sql_oper = sql_oper & 0 & ",'"                               'Cod_Parec_Geral
    sql_oper = sql_oper & LblBranco & "','"                      'Dsc_Parec_Derie1
    sql_oper = sql_oper & LblBranco & "',"                       'Dsc_Parec_Derie2
    sql_oper = sql_oper & 0 & ","                                'Num_Syas
    sql_oper = sql_oper & 0 & ","                                'Dat_Insp
    sql_oper = sql_oper & 0 & ",'"                               'Cod_Ocup_Seg
    sql_oper = sql_oper & TxtNum_notsso_num & "',"               'Num_Nosso_Num - num_notsso_num
    sql_oper = sql_oper & 0 & ",'"                               'Cod_Dept_Liber
    sql_oper = sql_oper & LblBranco & "',"                       'Cod_User_Liber
    sql_oper = sql_oper & 0 & ","                                'Dat_Liber_Insp
    sql_oper = sql_oper & 0 & ","                                'Hor_Liber_Insp
    sql_oper = sql_oper & 0 & ",'"                               'Cod_LiberEmis
    sql_oper = sql_oper & Trim(LCdAtiv) & "','"                  'Cod_Ativ
    sql_oper = sql_oper & Trim(LCdAtivCompl) & "',"              'Cod_Ativ_Compl
    sql_oper = sql_oper & Trim(PDatEmis) & ","                   'Dat_Emis_Apol_Endo
    sql_oper = sql_oper & Cdblx(txtNumInspAnt) & ","                    'Num_Insp_Ant
    sql_oper = sql_oper & "'" & LblBranco & "',"                 'E_Mail_Terc
    sql_oper = sql_oper & Trim(LAnoMes) & ","                    'Mes/Ano"
    sql_oper = sql_oper & Cdblx(TxtNumCotacao) & ","             'NumCotacao [bigint] NOT NULL ,
    sql_oper = sql_oper & Cdblx(txtNumProposta) & ","            'NumProposta [bigint] NOT NULL ,
    If IsDate(txtDataRec) Then
        sql_oper = sql_oper & Format(txtDataRec, "yyyymmdd") & ","   'DataReceb [bigint] NOT NULL ,
    Else
        sql_oper = sql_oper & "0,"                              'DataReceb [bigint] NOT NULL ,
    End If
    sql_oper = sql_oper & Cdblx(TxtEndNumero) & ","             'Num_Logr [int] NOT NULL ,
    sql_oper = sql_oper & "'" & Space(4) & "',"                 'cod_clasf_d [char] (4) ,
    sql_oper = sql_oper & "'" & Space(2) & "',"                 'cod_compl_clasf_d [char] (2) ,
    sql_oper = sql_oper & "' '" & ","                           'AtivDivergente [Char](1) ,
    sql_oper = sql_oper & "' '" & ","                           'EndDivergente [Char](1) ,
    sql_oper = sql_oper & "' '" & ","                           'Dsc_EndDiver [Char](40) ,
    sql_oper = sql_oper & "' '" & ","                           'Tip_Cancel [Char](1) ,
    sql_oper = sql_oper & "0" & ","                             'DataInspec [bigint] NOT NULL
    sql_oper = sql_oper & IIf(chkflg_confid.Value, 1, 0) & ","  'flg_confid [int] NULL
    sql_oper = sql_oper & "0" & ","                             'Dat_Agend [bigint] NULL
    sql_oper = sql_oper & Cdblx(TxtNumItem) & ","               'Num_Item [numeric] NULL
    If Left(CmbRamo.Text, 3) = "300" Or Left(CmbRamo.Text, 3) = "620" Or Left(CmbRamo.Text, 3) = "710" Then
        vetor = Split(cboEquip.Text, ";")
        sql_oper = sql_oper & Cdblx(vetor(1)) & ","             'NUM_EQUIP decimal(5) null,
        sql_oper = sql_oper & "'" & Trim(vetor(0)) & "',"       'NOME_EQUIP varchar(60) null,
        sql_oper = sql_oper & Cdblx(vetor(2)) & ","             'TIPO_EQUIP decimal(2) null,
        sql_oper = sql_oper & Cdblx(vetor(3)) & ","             'CLASSE_EQUIP decimal(2) null,
        sql_oper = sql_oper & Cdblx(TxtAnoEquip.Text) & ","     'ANO_FABRICACAO_EQUIP decimal(4) null,
        sql_oper = sql_oper & "'" & txtMarcaEquip.Text & "',"   'MARCA_EQUIP varchar(40) null,
        sql_oper = sql_oper & "'" & txtModeloEquip.Text & "',"  'MODELO_EQUIP varchar(40) null,
        sql_oper = sql_oper & "'" & txtSerieEquip.Text & "',"   'NUM_SERIE_EQUIP varchar(20) null
        sql_oper = sql_oper & "'" & txtChassiEquip.Text & "'"   'NUM_CHASSI_EQUIP varchar(20) null
    Else
        sql_oper = sql_oper & "0,"                              'NUM_EQUIP decimal(5) null,
        sql_oper = sql_oper & "'',"                             'NOME_EQUIP varchar(60) null,
        sql_oper = sql_oper & "0,"                              'TIPO_EQUIP decimal(2) null,
        sql_oper = sql_oper & "0,"                              'CLASSE_EQUIP decimal(2) null,
        sql_oper = sql_oper & "0,"                              'ANO_FABRICACAO_EQUIP decimal(4) null,
        sql_oper = sql_oper & "'',"                             'MARCA_EQUIP varchar(40) null,
        sql_oper = sql_oper & "'',"                             'MODELO_EQUIP varchar(40) null,
        sql_oper = sql_oper & "'',"                             'NUM_SERIE_EQUIP varchar(20) null
        sql_oper = sql_oper & "''"                              'NUM_CHASSI_EQUIP varchar(20) null
    End If
    sql_oper = sql_oper & ")"     
	


	--Insert tabela Tab_Cobert_Insp
	" insert into Inspecao..Tab_Cobert_Insp"
	" select a.num_insp, b.COD_COBERT, b.VAL_IS, 0 as Cod_Bloco"
	" from Inspecao..tab_insp a (nolock)"
	" join RamosDiversos..Tab_Ped_Cobert b (nolock) on b.NOSSO_NUMERO = a.num_notsso_num and b.NUM_ITEM = a.num_item"
	" where a.num_insp = " & pNum_insp


	-- tabela Tab_DistrInsp   
	"Insert into Inspecao..Tab_DistrInsp "
    " (Num_Insp, CepRisco, NumPrest, EPrest, DatDigit, DatEnvio, Enviado, Roubo )"
    " Values ("
    RstDistr!Num_insp & ", "
    RstDistr!Cep & ", "
    "0, "
    "'SYASWEB', "
    RstDistr!Dat_Cadastro & ","
    "0, "
    "' ', "
    "' ')"