   M S B A M L   `   `   `  ����E  AGymManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:GymManager
GymManager  5          GymManager.managePackages   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:GymManager     5        N� Title$  managePackages��5	      .��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��packagesList��5         AutoGenerateColumns .  $
��Left=�6C   $	��649��6^   $	��Top=�6k   $
��1108��6�      
IsReadOnly . $��0,20,0,0q�6�      	CellStyle 5      �� 5       �� 
TargetType*  $System.Windows.Controls.DataGridCell"  6   .�5      -   5        5         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Nazwa��5   !   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5   %   �� *Name+   HeaderTemplate 5      �� 5      .R�5      �� 	 �� Orientation$	 
Horizontal=�6)   .+�5   "   �� ��6,   �� *	Content+
 �� RelativeSource
 ��  �� Mode$ TemplatedParent=��� 5   "    ��Name$ txtName��6*   -   5        $	��100��5   ;   6G   5      5      5        5       $
 Cena��5!   !    5    %   �� *Price+ 5"      �� 5#      .R�5$      �� $	 
Horizontal=�6)   .+�5%   "   �� ��6,   �� *	Content+
 �� $ TemplatedParent=��� 5'   "   $ txtPrice��6*   -   5        $	��100��5'   <   6H   5(      5)      5+        5,      $ Czas trwania��5-   !    5,   %   �� *	TimeLimit+ 5.      �� 5/      .R�50      �� $	 
Horizontal=�6)   .+�51   "   �� ��6,   �� *	Content+
 �� $ TemplatedParent=��� 53   "   $ txtTimeLimit��6*   -   5        $	��100��53   @   6L   54      55      57        58      $ Limit wejść��59   !    58   %   �� *EntrancesLimit+ 5:      �� 5;      .R�5<      �� $	 
Horizontal=�6)   .+�5=   "   �� ��6,   �� *	Content+
 �� $ TemplatedParent=��� 5?   "   $ txtEntrancesLimit��6*   -   5        $	��100��5?   E   6Q   5@      5A      5C      5E      �� 5F   
   -   5        $
��Nowy��5F   $   $
��Left=�63   $	��Top=�6N   $��75��6f   5H      5I      