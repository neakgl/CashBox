import { useEffect, useState, useCallback } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

function History() {
  const [transactions, setTransactions] = useState([]);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  const fetchHistory = useCallback(async () => {
    try {
      const token = localStorage.getItem("token");
      if (!token) {
        navigate('/');
        return;
      }

      const incomeReq = axios.post("https://localhost:44339/api/Incomes/getAllIncomes", {}, { headers: { Authorization: `Bearer ${token}` } });
      const expenseReq = axios.post("https://localhost:44339/api/Expenses/getAllExpenses", {}, { headers: { Authorization: `Bearer ${token}` } });

      const [incomeRes, expenseRes] = await Promise.all([incomeReq, expenseReq]);

      const incomes = (incomeRes.data || []).map(item => ({
        ...item,
        type: 'Gelir',
        displayTitle: item.title, 
      }));

      
      const expenses = (expenseRes.data || []).map(item => ({
        ...item,
        type: 'Gider',
        displayTitle: item.categoryName, 
      }));

      const combinedList = [...incomes, ...expenses].sort((a, b) => {
        return new Date(b.date) - new Date(a.date);
      });

      setTransactions(combinedList);
      setLoading(false);

    } catch (error) {
      console.error("Geçmiş çekilemedi:", error);
      setLoading(false);
    }
  }, [navigate]);

  useEffect(() => {
    fetchHistory();
  }, [fetchHistory]);

  if (loading) return <div style={{ padding: '50px', textAlign: 'center', fontSize: '1.2rem' }}>İşlemler yükleniyor... ⏳</div>;

  return (
    <div style={{ padding: '30px', fontFamily: 'sans-serif', backgroundColor: '#f4f7f6', minHeight: '100vh' }}>
      
      {/* Üst Bilgi ve Navigasyon */}
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '30px' }}>
        <h2 style={{ color: '#2c3e50', margin: 0 }}>📜 İşlem Geçmişim</h2>
        <button onClick={() => navigate('/dashboard')} style={{ backgroundColor: '#007bff', color: 'white', border: 'none', padding: '10px 20px', borderRadius: '8px', cursor: 'pointer', fontWeight: 'bold' }}>
          ⬅️ Özet'e Dön
        </button>
      </div>

      {/* Tablo Alanı */}
      <div style={{ background: 'white', padding: '20px', borderRadius: '15px', boxShadow: '0 4px 15px rgba(0,0,0,0.05)', overflowX: 'auto' }}>
        {transactions.length > 0 ? (
          <table style={{ width: '100%', borderCollapse: 'collapse', textAlign: 'left' }}>
            <thead>
              <tr style={{ backgroundColor: '#f8f9fa', borderBottom: '2px solid #dee2e6' }}>
                <th style={{ padding: '15px', color: '#495057' }}>Tarih</th>
                <th style={{ padding: '15px', color: '#495057' }}>Tür</th>
                <th style={{ padding: '15px', color: '#495057' }}>Kategori / Başlık</th>
                <th style={{ padding: '15px', color: '#495057' }}>Açıklama</th>
                <th style={{ padding: '15px', color: '#495057', textAlign: 'right' }}>Tutar</th>
              </tr>
            </thead>
            <tbody>
              {transactions.map((t, index) => (
                <tr key={index} style={{ borderBottom: '1px solid #eee' }}>
                  <td style={{ padding: '15px', color: '#555' }}>
                    {new Date(t.date).toLocaleDateString('tr-TR', { day: '2-digit', month: 'short', year: 'numeric', hour: '2-digit', minute: '2-digit' })}
                  </td>
                  <td style={{ padding: '15px' }}>
                    <span style={{ 
                      backgroundColor: t.type === 'Gelir' ? '#e9f7ef' : '#fdf2f0', 
                      color: t.type === 'Gelir' ? '#27ae60' : '#e74c3c', 
                      padding: '5px 10px', 
                      borderRadius: '15px', 
                      fontWeight: 'bold',
                      fontSize: '0.85rem'
                    }}>
                      {t.type}
                    </span>
                  </td>
                  <td style={{ padding: '15px', fontWeight: 'bold', color: '#333' }}>{t.displayTitle}</td>
                  <td style={{ padding: '15px', color: '#777' }}>{t.description || "-"}</td>
                  <td style={{ padding: '15px', textAlign: 'right', fontWeight: 'bold', color: t.type === 'Gelir' ? '#27ae60' : '#e74c3c', fontSize: '1.1rem' }}>
                    {t.type === 'Gelir' ? '+' : '-'}{t.amount.toLocaleString()} ₺
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        ) : (
          <p style={{ textAlign: 'center', color: '#888', fontStyle: 'italic', padding: '20px' }}>Henüz hiçbir işlem bulunmuyor.</p>
        )}
      </div>

    </div>
  );
}
export default History;