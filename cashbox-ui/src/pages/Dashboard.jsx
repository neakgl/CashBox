import { useEffect, useState, useCallback } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

function Dashboard() {
  const [summary, setSummary] = useState(null);
  const [categories, setCategories] = useState([]); 
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();


  const [type, setType] = useState('gider'); 
  const [amount, setAmount] = useState('');
  const [description, setDescription] = useState('');
  
  
  const [title, setTitle] = useState('');
  
  
  const [categoryId, setCategoryId] = useState(''); 
  const [isSubmitting, setIsSubmitting] = useState(false);

  
  const fetchWalletData = useCallback(async () => {
    try {
      const token = localStorage.getItem("token");
      if (!token) return navigate('/');

      const response = await axios.post("https://localhost:44339/api/Wallets/getSummary", {}, { 
        headers: { Authorization: `Bearer ${token}` }
      });
      setSummary(response.data.data); 
    } catch (error) {
      console.error("Özet çekme hatası:", error);
    }
  }, [navigate]);

  
  const fetchCategories = useCallback(async () => {
    try {
      const token = localStorage.getItem("token");
      const response = await axios.post("https://localhost:44339/api/Categories/getAllCategories", {}, {
        headers: { Authorization: `Bearer ${token}` }
      });
      setCategories(response.data);
      
      if(response.data && response.data.length > 0) {
          setCategoryId(response.data[0].id);
      }
    } catch (error) {
      console.error("Kategoriler çekilemedi:", error);
    }
  }, []);

  
  useEffect(() => {
    const loadAllData = async () => {
      await fetchWalletData();
      await fetchCategories();
      setLoading(false);
    };
    loadAllData();
  }, [fetchWalletData, fetchCategories]);
  


  const handleAddTransaction = async (e) => {
    e.preventDefault();
    setIsSubmitting(true);

    try {
      const token = localStorage.getItem("token");
      
      if (type === 'gelir') {
        const payload = {
          title: title,
          amount: Number(amount),
          description: description
        };
        await axios.post("https://localhost:44339/api/Incomes/createIncome", payload, {
          headers: { Authorization: `Bearer ${token}` }
        });

      } else {
        const payload = {
          categoryId: Number(categoryId),
          amount: Number(amount),
          description: description
        };
        await axios.post("https://localhost:44339/api/Expenses/createExpense", payload, {
          headers: { Authorization: `Bearer ${token}` }
        });
      }

      setAmount('');
      setDescription('');
      setTitle('');
      alert(`${type === 'gelir' ? 'Gelir' : 'Gider'} başarıyla eklendi! 💸`);
      
      fetchWalletData(); 

    } catch (error) {
      console.error("İşlem ekleme hatası:", error.response?.data || error);
      alert("Hata: İşlem eklenemedi. " + (error.response?.data?.message || ""));
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleLogout = () => {
    localStorage.removeItem("token");
    navigate('/');
  };

  if (loading) return <div style={{ padding: '50px', textAlign: 'center', fontSize: '1.2rem' }}>Veriler yükleniyor... ⏳</div>;

  return (
    <div style={{ padding: '30px', fontFamily: 'sans-serif', backgroundColor: '#f4f7f6', minHeight: '100vh' }}>
      
      {}
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '30px' }}>
        <h2 style={{ color: '#2c3e50', margin: 0 }}>CashBox - Cüzdan Özeti 💰</h2>
        <div style={{ display: 'flex', gap: '15px' }}>
          <button onClick={() => navigate('/admin')} style={{ backgroundColor: '#2c3e50', color: 'white', border: 'none', padding: '10px 20px', borderRadius: '8px', cursor: 'pointer', fontWeight: 'bold' }}>
            ⚙️ Admin
          </button>
          <button onClick={() => navigate('/history')} style={{ backgroundColor: '#007bff', color: 'white', border: 'none', padding: '10px 20px', borderRadius: '8px', cursor: 'pointer', fontWeight: 'bold' }}>
            📜 İşlem Geçmişi
          </button>
          <button onClick={handleLogout} style={{ backgroundColor: '#dc3545', color: 'white', border: 'none', padding: '10px 20px', borderRadius: '8px', cursor: 'pointer', fontWeight: 'bold' }}>
            Çıkış Yap
          </button>
        </div>
      </div>

      <div style={{ display: 'flex', gap: '30px', flexWrap: 'wrap' }}>
        
        {/* SOL TARAF*/}
        <div style={{ flex: '2', minWidth: '300px' }}>
          <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(200px, 1fr))', gap: '20px' }}>
            <div style={{ background: 'white', padding: '25px', borderRadius: '15px', boxShadow: '0 4px 15px rgba(0,0,0,0.05)' }}>
              <p style={{ color: '#888', marginBottom: '10px', fontWeight: 'bold', fontSize: '0.9rem' }}>NET BAKİYE</p>
              <h2 style={{ margin: 0, color: '#2c3e50', fontSize: '2rem' }}>{(summary?.netBalance ?? 0).toLocaleString()} ₺</h2>
            </div>
            <div style={{ background: 'white', padding: '25px', borderRadius: '15px', boxShadow: '0 4px 15px rgba(0,0,0,0.05)' }}>
              <p style={{ color: '#888', marginBottom: '10px', fontWeight: 'bold', fontSize: '0.9rem' }}>TOPLAM GELİR</p>
              <h2 style={{ margin: 0, color: '#27ae60', fontSize: '2rem' }}>{(summary?.totalIncome ?? 0).toLocaleString()} ₺</h2>
            </div>
            <div style={{ background: 'white', padding: '25px', borderRadius: '15px', boxShadow: '0 4px 15px rgba(0,0,0,0.05)' }}>
              <p style={{ color: '#888', marginBottom: '10px', fontWeight: 'bold', fontSize: '0.9rem' }}>TOPLAM GİDER</p>
              <h2 style={{ margin: 0, color: '#e74c3c', fontSize: '2rem' }}>{(summary?.totalExpense ?? 0).toLocaleString()} ₺</h2>
            </div>
          </div>

          {/* KATEGORİ DAĞILIMI */}
          <div style={{ marginTop: '30px', background: 'white', padding: '30px', borderRadius: '15px', boxShadow: '0 4px 15px rgba(0,0,0,0.05)' }}>
            <h3 style={{ color: '#2c3e50', borderBottom: '2px solid #f0f0f0', paddingBottom: '15px', marginTop: 0 }}>Harcama Dağılımı 📊</h3>
            {summary?.categoryPercentages && summary.categoryPercentages.length > 0 ? (
              <div style={{ display: 'flex', flexDirection: 'column', gap: '15px', marginTop: '20px' }}>
                {summary.categoryPercentages.map((item, index) => (
                  <div key={index} style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', padding: '15px', backgroundColor: '#f8f9fa', borderRadius: '10px', border: '1px solid #eee' }}>
                    <span style={{ fontWeight: 'bold', color: '#555', textTransform: 'uppercase' }}>🏷️ {item.categoryName}</span>
                    <div style={{ display: 'flex', alignItems: 'center', gap: '15px' }}>
                      <span style={{ color: '#e74c3c', fontWeight: 'bold', fontSize: '1.1rem' }}>{item.totalAmount.toLocaleString()} ₺</span>
                      <span style={{ background: '#f39c12', color: 'white', padding: '5px 12px', borderRadius: '20px', fontWeight: 'bold', fontSize: '0.85rem' }}>% {item.percentage}</span>
                    </div>
                  </div>
                ))}
              </div>
            ) : (
              <p style={{ color: '#888', fontStyle: 'italic', marginTop: '20px' }}>Harcama verisi bulunmuyor.</p>
            )}
          </div>
        </div>

        {/* SAĞ TARAF: FORM */}
        <div style={{ flex: '1', minWidth: '300px', background: 'white', padding: '30px', borderRadius: '15px', boxShadow: '0 4px 15px rgba(0,0,0,0.05)', height: 'fit-content' }}>
          <h3 style={{ color: '#2c3e50', borderBottom: '2px solid #f0f0f0', paddingBottom: '15px', marginTop: 0 }}>Yeni İşlem Ekle ➕</h3>
          
          <form onSubmit={handleAddTransaction} style={{ display: 'flex', flexDirection: 'column', gap: '15px', marginTop: '20px' }}>
            
            {/* BUTONLAR */}
            <div style={{ display: 'flex', gap: '10px' }}>
              <button type="button" onClick={() => setType('gelir')} style={{ flex: 1, padding: '10px', borderRadius: '8px', border: type === 'gelir' ? '2px solid #27ae60' : '1px solid #ddd', backgroundColor: type === 'gelir' ? '#e9f7ef' : 'white', color: type === 'gelir' ? '#27ae60' : '#888', fontWeight: 'bold', cursor: 'pointer' }}>
                Gelir
              </button>
              <button type="button" onClick={() => setType('gider')} style={{ flex: 1, padding: '10px', borderRadius: '8px', border: type === 'gider' ? '2px solid #e74c3c' : '1px solid #ddd', backgroundColor: type === 'gider' ? '#fdf2f0' : 'white', color: type === 'gider' ? '#e74c3c' : '#888', fontWeight: 'bold', cursor: 'pointer' }}>
                Gider
              </button>
            </div>

            {/* SADECE GELİR İSE BAŞLIK GÖSTER */}
            {type === 'gelir' && (
               <div>
                 <label style={{ fontSize: '0.9rem', color: '#555', fontWeight: 'bold' }}>Gelir Başlığı</label>
                 <input type="text" value={title} onChange={(e) => setTitle(e.target.value)} required placeholder="Örn: Maaş, Prim..." style={{ width: '100%', padding: '10px', marginTop: '5px', borderRadius: '8px', border: '1px solid #ccc' }} />
               </div>
            )}

            {/* SADECE GİDER İSE KATEGORİ GÖSTER */}
            {type === 'gider' && (
              <div>
                <label style={{ fontSize: '0.9rem', color: '#555', fontWeight: 'bold' }}>Kategori</label>
                <select value={categoryId} onChange={(e) => setCategoryId(e.target.value)} required style={{ width: '100%', padding: '10px', marginTop: '5px', borderRadius: '8px', border: '1px solid #ccc', backgroundColor: 'white' }}>
                  {categories.map((cat) => (
                    <option key={cat.id} value={cat.id}>{cat.name}</option>
                  ))}
                </select>
              </div>
            )}

            {/* ORTAK ALANLAR */}
            <div>
              <label style={{ fontSize: '0.9rem', color: '#555', fontWeight: 'bold' }}>Tutar (₺)</label>
              <input type="number" value={amount} onChange={(e) => setAmount(e.target.value)} required placeholder="Örn: 1500" style={{ width: '100%', padding: '10px', marginTop: '5px', borderRadius: '8px', border: '1px solid #ccc' }} />
            </div>

            <div>
              <label style={{ fontSize: '0.9rem', color: '#555', fontWeight: 'bold' }}>Açıklama</label>
              <input type="text" value={description} onChange={(e) => setDescription(e.target.value)} required placeholder="Kısa bir açıklama..." style={{ width: '100%', padding: '10px', marginTop: '5px', borderRadius: '8px', border: '1px solid #ccc' }} />
            </div>

            <button type="submit" disabled={isSubmitting} style={{ padding: '12px', backgroundColor: type === 'gelir' ? '#27ae60' : '#e74c3c', color: 'white', border: 'none', borderRadius: '8px', cursor: isSubmitting ? 'not-allowed' : 'pointer', fontWeight: 'bold', fontSize: '1rem', marginTop: '10px' }}>
              {isSubmitting ? 'Ekleniyor...' : (type === 'gelir' ? 'Geliri Kaydet' : 'Gideri Kaydet')}
            </button>
            
          </form>
        </div>

      </div>
    </div>
  );
}

export default Dashboard;